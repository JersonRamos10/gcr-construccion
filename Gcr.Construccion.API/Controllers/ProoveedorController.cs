using Microsoft.AspNetCore.Mvc;
using Gcr.Construccion.API.Interfaces;
using Gcr.Construccion.API.DTOs;

namespace Gcr.Construccion.API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        //inyeccion de dependencias del servicio
        private readonly IProveedorService _proveedorService;

        //constructor que recibe las dependencias inyectadas
        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProovedor()
        {
            var proveedores = await _proveedorService.GetAllAsync();
            return Ok(proveedores);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProveedorById(int id)
        {
            var proveedor = await _proveedorService.GetByIdAsync(id);
            if (proveedor == null) return NotFound();
            return Ok(proveedor);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProveedor([FromBody] ProveedorCreateDto dto)
        {
            var proveedor = await _proveedorService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetProveedorById), new { id = proveedor.Id }, proveedor);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var eliminado = await _proveedorService.DeleteAsync(id);
            if (!eliminado) return NotFound();

            return NoContent();
        }

    }
}