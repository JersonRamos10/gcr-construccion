using Microsoft.AspNetCore.Mvc;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaMaterialController : ControllerBase
    {
        private readonly ICategoriaMaterialService _service; 

        public CategoriaMaterialController(ICategoriaMaterialService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            var categorias = await _service.GetAllAsync();
            if(categorias.Count() < 0)
            {
                return NoContent();
            }
            return Ok(categorias);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCategoriaById(int id)
        {
            var categoriaEncontrada = await _service.GetByIdAsync(id);
            if (categoriaEncontrada == null) return NotFound();
            return Ok(categoriaEncontrada);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriaMaterialCreateDto dto)
        {
            var categoria = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetCategoriaById), new { id = categoria.Id }, categoria);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }

    }

}
