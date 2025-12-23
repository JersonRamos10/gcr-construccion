using Microsoft.AspNetCore.Mvc;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoManoDeObraController : ControllerBase
    {
        private readonly IPagoManoDeObraService _service;

        public PagoManoDeObraController(IPagoManoDeObraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] int? empleadoId = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null
        )
        {
            var result = await _service.GetAllAsync(page, pageSize, empleadoId, fromDate, toDate);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _service.GetByIdAsync(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PagoManoDeObraCreateDto dto)
        {
            try
            {
                var pago = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = pago.Id }, pago);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
