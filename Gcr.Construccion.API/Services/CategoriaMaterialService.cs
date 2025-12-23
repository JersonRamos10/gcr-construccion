using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.Entities;

namespace Gcr.Construccion.API.Services
{
    public class CategoriaMaterialService : ICategoriaMaterialService
    {
         //inyeccion de dependencias 

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public CategoriaMaterialService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoriaMaterialDto> CreateAsync(CategoriaMaterialCreateDto dto)
        {
            //Mapeo del dto a la entidad
            var categoria = _mapper.Map<CategoriaMaterial>(dto);

            await _context.Categorias.AddAsync(categoria);

            await _context.SaveChangesAsync();

            var categoriaDto = _mapper.Map<CategoriaMaterialDto>(categoria);

            return categoriaDto;

        }

        public async Task<bool> DeleteAsync(int id)
        {
             //Busqueda de la categoria por id
            var categoriaEncontrada = await _context.Categorias.FindAsync(id);

            if (categoriaEncontrada == null)
            {
                return false;
            }

            //Eliminacion de la categoria por su id 
            _context.Categorias.Remove(categoriaEncontrada);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CategoriaMaterialDto>> GetAllAsync()
        {
            var categoriaMaterial = await _context.Categorias.ToListAsync();

            //Mapeo de la entidad a dto
            var categoriaDto = _mapper.Map<IEnumerable<CategoriaMaterialDto>>(categoriaMaterial);

            return categoriaDto;
        }

        public async Task<CategoriaMaterialDto?> GetByIdAsync(int id)
        {
            var busquedaCategoria = await _context.Categorias.FindAsync(id);

            if (busquedaCategoria == null)
            {
                return null;
            }

            var categoriaDto = _mapper.Map<CategoriaMaterialDto>(busquedaCategoria);

            return categoriaDto;
        }
    }
}