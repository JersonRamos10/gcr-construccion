using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.Entities;
using Gcr.Construccion.API.DTOs;
using AutoMapper;
using Gcr.Construccion.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gcr.Construccion.API.Services
{
    public class IngresoService : IIngresoService
    {
        // inyeccion de dependencias del contexto y el mapper
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        // constructor que recibe las dependencias inyectadas
        public IngresoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<IngresoDto>> GetAllAsync(
            int page,
            int pageSize,
            DateTime? fromDate,
            DateTime? toDate
        )
        {
            // Seguridad 
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 5;

            // Query base
            var query = _context.Ingresos.AsQueryable();

            // Filtro por fecha desde
            if (fromDate.HasValue)
            {
                query = query.Where(i => i.Fecha >= fromDate.Value);
            }

            // Filtro por fecha hasta
            if (toDate.HasValue)
            {
                query = query.Where(i => i.Fecha <= toDate.Value);
            }

            // Total antes de paginar
            var totalItems = await query.CountAsync();

            // Paginación
            var ingresos = await query
                .OrderByDescending(i => i.Fecha)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Mapeo
            var ingresosDto = _mapper.Map<IEnumerable<IngresoDto>>(ingresos);

            // Resultado paginado
            return new PagedResultDto<IngresoDto>
            {
                Items = ingresosDto,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
        }
        public async Task<IngresoDto?> GetByIdAsync(int id)
        {
            //Busqueda del ingreso por id
            var ingreso = await _context.Ingresos.FindAsync(id);

            if (ingreso == null)
            {
                return null;
            }
            //Mapeo de la entidad a dto
            var ingresoDto = _mapper.Map<IngresoDto>(ingreso);

            //retorno del dto mapeado
            return ingresoDto;
        }

        public async Task<IngresoDto> CreateAsync(IngresoCreateDto dto)
        {
            //Mapeo del dto a la entidad
            var ingreso = _mapper.Map<Ingreso>(dto);

            //Agregar el ingreso al contexto
            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();

            //Mapeo de la entidad a dto
            var ingresoDto = _mapper.Map<IngresoDto>(ingreso);

            //retorno del dto mapeado
            return ingresoDto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //Busqueda del ingreso por id
            var ingreso = await _context.Ingresos.FindAsync(id);

            if (ingreso == null)
            {
                return false;
            }

            //Eliminacion del ingreso
            _context.Ingresos.Remove(ingreso);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
