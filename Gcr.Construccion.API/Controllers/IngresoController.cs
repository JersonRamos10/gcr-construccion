using Microsoft.AspNetCore.Mvc;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngresoController : ControllerBase
    {
        //inyeccion de dependencias del servicio
        private readonly IIngresoService _ingresoService;

        //constructor que recibe las dependencias inyectadas
        public IngresoController(IIngresoService ingresoService)
        {
            _ingresoService = ingresoService;
        }

        // para obtener todos los ingresos peticion: GET api/ingreso
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 5,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null
        )
        {
            var result = await _ingresoService.GetAllAsync(
                page, pageSize, fromDate, toDate
            );

            return Ok(result);
        }

        // para obtener un ingreso por id peticion: GET api/ingreso/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngresoById(int id)
        {
            var ingreso = await _ingresoService.GetByIdAsync(id);

            if (ingreso == null)
            {
                return NotFound();
            }

            return Ok(ingreso);
        }

        // para crear un nuevo ingreso peticion: POST api/ingreso
        [HttpPost]
        public async Task<IActionResult> CreateIngreso([FromBody] IngresoCreateDto dto)
        {
            var ingreso = await _ingresoService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetIngresoById), new { id = ingreso.Id }, ingreso);
        }

        //delete de ingreso peticion: DELETE api/ingreso/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            var ingreso = await _ingresoService.DeleteAsync(id);

            if (!ingreso)
            {
                return NotFound();
            }
            return NoContent();
        }

    }

}