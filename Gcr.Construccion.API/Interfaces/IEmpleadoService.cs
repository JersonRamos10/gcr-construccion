using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<EmpleadoDto>> GetAllAsync(); //Lista de todos los empleados

        Task<EmpleadoDto?> GetByIdAsync(int id); // obtiene un empleado por su Id

        Task<EmpleadoDto> CreateAsync(EmpleadoCreateDto dto); // Crea un empleado

        Task<bool> UpdateAsync(int id, EmpleadoUpdateDto dto);

        Task<bool> DeleteAsync(int id); //Elemina un empleado
    }
}