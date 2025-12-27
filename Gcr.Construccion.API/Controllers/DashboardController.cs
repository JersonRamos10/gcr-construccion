using Microsoft.AspNetCore.Mvc;
using Gcr.Construccion.API.Interfaces;

namespace Gcr.Construccion.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        // GET: /api/dashboard/resumen
        [HttpGet("resumen")]
        public async Task<IActionResult> GetSummary( [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null
        )
        {
            var result = await _service.GetSummaryAsync(fromDate, toDate);
            return Ok(result);
        }
    }
}
