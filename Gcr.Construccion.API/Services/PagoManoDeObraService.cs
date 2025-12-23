using AutoMapper;
using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;
using Gcr.Construccion.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gcr.Construccion.API.Services
{
    public class PagoManoDeObraService : IPagoManoDeObraService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PagoManoDeObraService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<PagoManoDeObraDto>> GetAllAsync(
            int page,
            int pageSize,
            int? empleadoId,
            DateTime? fromDate,
            DateTime? toDate
        )
        {
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 5;

            var query = _context.PagosEmpleados
                .Include(p => p.Empleado)
                .AsQueryable();

            if (empleadoId.HasValue)
                query = query.Where(p => p.EmpleadoId == empleadoId.Value);

            if (fromDate.HasValue)
                query = query.Where(p => p.FechaPago >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(p => p.FechaPago <= toDate.Value);

            var totalItems = await query.CountAsync();

            var pagos = await query
                .OrderByDescending(p => p.FechaPago)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagosDto = _mapper.Map<IEnumerable<PagoManoDeObraDto>>(pagos);

            return new PagedResultDto<PagoManoDeObraDto>
            {
                Items = pagosDto,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }

        public async Task<PagoManoDeObraDto?> GetByIdAsync(int id)
        {
            var pago = await _context.PagosEmpleados
                .Include(p => p.Empleado)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pago == null) return null;

            return _mapper.Map<PagoManoDeObraDto>(pago);
        }

        public async Task<PagoManoDeObraDto> CreateAsync(PagoManoDeObraCreateDto dto)
        {
            if (dto.DiasTrabajados <= 0 || dto.DiasTrabajados > 6)
                throw new ArgumentException("Los días trabajados deben estar entre 1 y 6.");

            var empleado = await _context.Empleados.FindAsync(dto.EmpleadoId);
            if (empleado == null)
                throw new ArgumentException("Empleado no existe.");

            var pago = _mapper.Map<PagoManoDeObra>(dto);

            // Lógica de negocio
            pago.TotalPagado = dto.DiasTrabajados * empleado.PagoPorDia;

            _context.PagosEmpleados.Add(pago);
            await _context.SaveChangesAsync();

            var pagoCreado = await _context.PagosEmpleados
                .Include(p => p.Empleado)
                .FirstAsync(p => p.Id == pago.Id);

            return _mapper.Map<PagoManoDeObraDto>(pagoCreado);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var pago = await _context.PagosEmpleados.FindAsync(id);
            if (pago == null) return false;

            _context.PagosEmpleados.Remove(pago);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
