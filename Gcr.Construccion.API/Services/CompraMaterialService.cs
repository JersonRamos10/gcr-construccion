using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.Entities;

namespace Gcr.Construccion.API.Services
{
    public class CompraMaterialService : ICompraMaterialService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMapper _mapper;

        public CompraMaterialService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CompraMaterialDto> CreateAsync(CompraMaterialCreateDto dto)
        {
            var compra = _mapper.Map<CompraMaterial>(dto);

            // Lógica de negocio
            compra.MontoTotal = dto.Cantidad * dto.PrecioUnitario;

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            // Recargar con relaciones para devolver nombres
            var compraCreada = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.CategoriaMaterial)
                .FirstAsync(c => c.Id == compra.Id);

            return _mapper.Map<CompraMaterialDto>(compraCreada);
        }

        public async Task<bool> UpdateAsync(int id, CompraMaterialUpdateDto dto)
        {
            var compra = await _context.Compras
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
                return false;

            // Actualizando los campos
            compra.Nombre = dto.Nombre;
            compra.Cantidad = dto.Cantidad;
            compra.PrecioUnitario = dto.PrecioUnitario;
            compra.FechaCompra = dto.FechaCompra;
            compra.ProveedorId = dto.ProveedorId;
            compra.CategoriaMaterialId = dto.CategoriaMaterialId;

            
            compra.MontoTotal = dto.Cantidad * dto.PrecioUnitario;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var compra = await _context.Compras.FindAsync(id);

            if (compra == null)
                return false;

            _context.Compras.Remove(compra);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<PagedResultDto<CompraMaterialDto>> GetAllAsync(
        int page,
        int pageSize,
        string? search,
        DateTime? fromDate,
        DateTime? toDate
    )
    {

        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 5;

        // Query base con relaciones
        var query = _context.Compras
            .Include(c => c.Proveedor)
            .Include(c => c.CategoriaMaterial)
            .AsQueryable();

        //  Filtro por texto (Nombre)
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(c =>
                c.Nombre.Contains(search)
            );
        }

        //  Filtro por fecha desde
        if (fromDate.HasValue)
        {
            query = query.Where(c =>
                c.FechaCompra >= fromDate.Value
            );
        }

        //  Filtro por fecha hasta
        if (toDate.HasValue)
        {
            query = query.Where(c =>
                c.FechaCompra <= toDate.Value
            );
        }

        //  Total de registros (antes de paginar)
        var totalItems = await query.CountAsync();

        // Aplicacion de la paginación
        var compras = await query
            .OrderByDescending(c => c.FechaCompra)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Mapear a DTO
        var comprasDto = _mapper.Map<IEnumerable<CompraMaterialDto>>(compras);

        //  Construir resultado paginado
        var result = new PagedResultDto<CompraMaterialDto>
        {
            Items = comprasDto,
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };

        return result;
    }

        public async Task<CompraMaterialDto?> GetByIdAsync(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.CategoriaMaterial)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
                return null;

            return _mapper.Map<CompraMaterialDto>(compra);
        }

    }
}