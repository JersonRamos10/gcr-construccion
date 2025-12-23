using Microsoft.AspNetCore.Mvc;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CompraMaterialController : ControllerBase
    {
        private readonly ICompraMaterialService _service;

        public CompraMaterialController(ICompraMaterialService service)
        {
            _service = service;
        }

        // GET: api/CompraMaterial
        [HttpGet]
        public async Task<IActionResult> GetAll( [FromQuery] int page = 1,[FromQuery] int pageSize = 5, [FromQuery] string? search = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null
        )
        {
            var result = await _service.GetAllAsync(
                page, pageSize, search, fromDate, toDate
            );

            return Ok(result);
        }

        // GET: api/CompraMaterial/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var compra = await _service.GetByIdAsync(id);

            if (compra == null)
                return NotFound();

            return Ok(compra);
        }

        // POST: api/CompraMaterial
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CompraMaterialCreateDto dto)
        {
            var compra = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = compra.Id }, compra);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] CompraMaterialUpdateDto dto
        )
        {
            var actualizado = await _service.UpdateAsync(id, dto);

            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/CompraMaterial/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _service.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
