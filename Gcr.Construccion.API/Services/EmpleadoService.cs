using AutoMapper;
using Gcr.Construccion.API.Data;
using Gcr.Construccion.API.DTOs;
using Gcr.Construccion.API.Entities;
using Gcr.Construccion.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gcr.Construccion.API.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EmpleadoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los empleados
        public async Task<IEnumerable<EmpleadoDto>> GetAllAsync()
        {
            var empleados = await _context.Empleados.OrderBy(e => e.NombreCompleto)
                                                    .ToListAsync();

            return _mapper.Map<IEnumerable<EmpleadoDto>>(empleados);
        }

        // Obtener empleado por Id
        public async Task<EmpleadoDto?> GetByIdAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return null;

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        // Crear empleado
        public async Task<EmpleadoDto> CreateAsync(EmpleadoCreateDto dto)
        {
            //Todo: Validaciones basicas
            if (string.IsNullOrWhiteSpace(dto.NombreCompleto))
                throw new ArgumentException("El nombre del empleado es obligatorio.");

            if (dto.PagoPorDia <= 0)
                throw new ArgumentException("El pago por día debe ser mayor a cero.");

            var empleado = _mapper.Map<Empleado>(dto);

            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        // Actualizar empleado
        public async Task<bool> UpdateAsync(int id, EmpleadoUpdateDto dto)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return false;

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(dto.NombreCompleto))
                throw new ArgumentException("El nombre del empleado es obligatorio.");

            if (dto.PagoPorDia <= 0)
                throw new ArgumentException("El pago por día debe ser mayor a cero.");

            // Mapeo de actualización
            _mapper.Map(dto, empleado);

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar empleado
        public async Task<bool> DeleteAsync(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);

            if (empleado == null)
                return false;

            _context.Empleados.Remove(empleado);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
