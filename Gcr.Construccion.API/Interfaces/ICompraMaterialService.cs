using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{
    public interface ICompraMaterialService
    {
        Task<PagedResultDto<CompraMaterialDto>> GetAllAsync(
            int page,
            int pageSize,
            string? search,
            DateTime? fromDate,
            DateTime? toDate
        );
        Task<CompraMaterialDto?> GetByIdAsync(int id);
        Task<CompraMaterialDto?> GetByNameAsync(string nombre);
        Task<CompraMaterialDto> CreateAsync(CompraMaterialCreateDto dto);

        Task<bool> UpdateAsync(int id, CompraMaterialUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}