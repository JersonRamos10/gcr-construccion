using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{
    public interface ICompraMaterialService
    {
        Task<IEnumerable<CompraMaterialDto>> GetAllAsync();
        Task<CompraMaterialDto?> GetByIdAsync(int id);
        Task<CompraMaterialDto> CreateAsync(CompraMaterialCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}