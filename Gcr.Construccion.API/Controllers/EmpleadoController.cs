using Microsoft.AspNetCore.Mvc;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        // GET: api/Empleado
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var empleados = await _empleadoService.GetAllAsync();
            return Ok(empleados);
        }

        // GET: api/Empleado/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empleado = await _empleadoService.GetByIdAsync(id);

            if (empleado == null)
                return NotFound();

            return Ok(empleado);
        }

        // POST: api/Empleado
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmpleadoCreateDto dto)
        {
            try
            {
                var empleado = await _empleadoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, empleado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Empleado/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoUpdateDto dto)
        {
            try
            {
                var actualizado = await _empleadoService.UpdateAsync(id, dto);

                if (!actualizado)
                    return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Empleado/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _empleadoService.DeleteAsync(id);

            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
