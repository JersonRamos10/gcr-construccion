using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync(
            DateTime? fromDate,
            DateTime? toDate
        );
    }
}
