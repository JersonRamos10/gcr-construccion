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

            _context.Compras.Add(compra);
            await _context.SaveChangesAsync();

            
            var compraCreada = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.Categoria)
                .FirstAsync(c => c.Id == compra.Id);

            return _mapper.Map<CompraMaterialDto>(compraCreada);
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

        public async Task<IEnumerable<CompraMaterialDto>> GetAllAsync()
        {
            var compras = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.Categoria)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CompraMaterialDto>>(compras);
        }

        public async Task<CompraMaterialDto?> GetByIdAsync(int id)
        {
            var compra = await _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.Categoria)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (compra == null)
                return null;

            return _mapper.Map<CompraMaterialDto>(compra);
        }

    }
}