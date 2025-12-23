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
        public async Task<IActionResult> GetAll()
        {
            var compras = await _service.GetAllAsync();
            return Ok(compras);
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
