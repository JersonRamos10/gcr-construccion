using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{
    public interface IIngresoService
    {
        //para listar los ingresos
        Task<IEnumerable<IngresoDto>> GetAllAsync();

        //para obtener uno por id
        Task<IngresoDto?> GetByIdAsync(int id);

        //para crear un nuevo ingreso
        Task<IngresoDto> CreateAsync(IngresoCreateDto dto);

        //para eliminar un ingreso por id
        Task<bool> DeleteAsync(int id);

    }
}
