
using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{
    public interface IProveedorService
    {

        Task<IEnumerable<ProveedorDto>> GetAllAsync(); //Lista de todos los proovedores

        Task<ProveedorDto?> GetByIdAsync(int id); // obtiene un proovedor por su Id

        Task<ProveedorDto> CreateAsync(ProveedorCreateDto dto); // Crea un provedor

        Task<bool> DeleteAsync(int id); //Elemina un proveedor

    }
}