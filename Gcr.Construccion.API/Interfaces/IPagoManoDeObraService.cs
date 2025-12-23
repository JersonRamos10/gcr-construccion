using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{
    public interface IPagoManoDeObraService
    {
        Task<PagedResultDto<PagoManoDeObraDto>> GetAllAsync(
            int page,
            int pageSize,
            int? empleadoId,
            DateTime? fromDate,
            DateTime? toDate
        );

        Task<PagoManoDeObraDto?> GetByIdAsync(int id);
        Task<PagoManoDeObraDto> CreateAsync(PagoManoDeObraCreateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
