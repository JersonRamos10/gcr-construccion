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
            // 1. Buscar o crear Categoria (igual que antes)
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nombre.ToLower() == dto.CategoriaNombre.ToLower());

            if (categoria == null)
            {
                categoria = new CategoriaMaterial { Nombre = dto.CategoriaNombre };
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
            }

            // 2. Buscar, crear O ACTUALIZAR Proveedor
            Proveedor proveedor = null;
            if (!string.IsNullOrWhiteSpace(dto.ProveedorNombre))
            {
                proveedor = await _context.Proveedores
                    .FirstOrDefaultAsync(p => p.Nombre.ToLower() == dto.ProveedorNombre.ToLower());

                if (proveedor == null)
                {
                    // CREAR NUEVO (si no existe)
                    proveedor = new Proveedor
                    {
                        Nombre = dto.ProveedorNombre,
                        Telefono = dto.ProveedorTelefono ?? string.Empty,
                        Direccion = dto.ProveedorDireccion ?? string.Empty
                    };
                    _context.Proveedores.Add(proveedor);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // ACTUALIZAR EXISTENTE (si ya existe y vienen datos nuevos)
                    bool huboCambios = false;

                    // Solo actualizamos si el usuario escribió algo en el campo de teléfono
                    if (!string.IsNullOrWhiteSpace(dto.ProveedorTelefono))
                    {
                        proveedor.Telefono = dto.ProveedorTelefono;
                        huboCambios = true;
                    }

                    // Solo actualizamos si el usuario escribió algo en el campo de dirección
                    if (!string.IsNullOrWhiteSpace(dto.ProveedorDireccion))
                    {
                        proveedor.Direccion = dto.ProveedorDireccion;
                        huboCambios = true;
                    }

                    if (huboCambios)
                    {
                        _context.Proveedores.Update(proveedor);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            // 3. Crear la compra (resto del código igual...)
            var compra = new CompraMaterial
            {
                Nombre = dto.Nombre,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario,
                FechaCompra = dto.FechaCompra,
                CategoriaMaterialId = categoria.Id,
                ProveedorId = proveedor?.Id,
                MontoTotal = dto.Cantidad * dto.PrecioUnitario,
                Medida = dto.Medida
            };

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            // Recargar con relaciones para devolver el DTO completo
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

            // Buscar o crear CategoriaMaterial por nombre
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Nombre.ToLower() == dto.CategoriaNombre.ToLower());

            if (categoria == null)
            {
                categoria = new CategoriaMaterial { Nombre = dto.CategoriaNombre };
                _context.Categorias.Add(categoria);
                await _context.SaveChangesAsync();
            }

            // Buscar o crear Proveedor por nombre (solo si el nombre no está vacío)
            Proveedor proveedor = null;
            if (!string.IsNullOrWhiteSpace(dto.ProveedorNombre))
            {
                proveedor = await _context.Proveedores
                    .FirstOrDefaultAsync(p => p.Nombre.ToLower() == dto.ProveedorNombre.ToLower());

                if (proveedor == null)
                {
                    proveedor = new Proveedor
                    {
                        Nombre = dto.ProveedorNombre,
                        Telefono = string.Empty,
                        Direccion = string.Empty
                    };
                    _context.Proveedores.Add(proveedor);
                    await _context.SaveChangesAsync();
                }
            }

            // Actualizando los campos
            compra.Nombre = dto.Nombre;
            compra.Cantidad = dto.Cantidad;
            compra.PrecioUnitario = dto.PrecioUnitario;
            compra.FechaCompra = dto.FechaCompra;
            compra.ProveedorId = proveedor?.Id;
            compra.CategoriaMaterialId = categoria.Id;
            compra.Medida = dto.Medida;
            compra.MontoTotal = dto.Cantidad * dto.PrecioUnitario;

            _context.Compras.Update(compra);
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
     string search,
     DateTime? fromDate,
     DateTime? toDate
 )
        {
            var query = _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.CategoriaMaterial)
                .AsQueryable();

            if (fromDate.HasValue)
                query = query.Where(c => c.FechaCompra >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(c => c.FechaCompra <= toDate.Value);

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(c => c.FechaCompra)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var dtoItems = _mapper.Map<IEnumerable<CompraMaterialDto>>(items);

            return new PagedResultDto<CompraMaterialDto>
            {
                Items = dtoItems,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
            };
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

        public async Task<CompraMaterialDto?> GetByNameAsync(string nombre)
        {
            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.CategoriaMaterial)
                .FirstOrDefaultAsync(c => c.Nombre.ToLower() == nombre.ToLower());

            if (compra == null)
                return null;

            return _mapper.Map<CompraMaterialDto>(compra);
        }

    }
}