using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.Entities;
using Gcr.Construccion.API.DTOs;
using AutoMapper;

using Gcr.Construccion.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gcr.Construccion.API.Services
{

    public class ProveedorService : IProveedorService

    {
        //inyeccion de dependencias 

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;


        public ProveedorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ProveedorDto> CreateAsync(ProveedorCreateDto dto)
        {
            //Mapeo del dto a la entidad
            var proveedor = _mapper.Map<Proveedor>(dto);

            await _context.Proveedores.AddAsync(proveedor);

            await _context.SaveChangesAsync();

            var proveedorDto = _mapper.Map<ProveedorDto>(proveedor);

            return proveedorDto;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            //Busqueda del ingreso por id
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
            {
                return false;
            }

            //Eliminacion del ingreso
            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProveedorDto>> GetAllAsync()
        {
            var proveedor = await _context.Proveedores.ToListAsync();

            //Mapeo de la entidad a dto
            var proveedorDto = _mapper.Map<IEnumerable<ProveedorDto>>(proveedor);

            return proveedorDto;
        }

        public async Task<ProveedorDto?> GetByIdAsync(int id)
        {
            var searchProveedor = await _context.Proveedores.FindAsync(id);

            if (searchProveedor == null)
            {
                return null;
            }

            var proveedorDto = _mapper.Map<ProveedorDto>(searchProveedor);

            return proveedorDto;
        }
    }   

}