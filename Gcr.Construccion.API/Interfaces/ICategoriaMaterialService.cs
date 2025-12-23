using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{

    public interface ICategoriaMaterialService
    {

        Task<IEnumerable<CategoriaMaterialDto>> GetAllAsync();

        Task<CategoriaMaterialDto?> GetByIdAsync(int id);

        Task<CategoriaMaterialDto> CreateAsync(CategoriaMaterialCreateDto dto);

        Task<bool> DeleteAsync(int id);

    }
}