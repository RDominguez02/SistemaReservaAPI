    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservaAPI.Models;
using SistemaReservaAPI.Server.Models;

namespace SistemaReservaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class
        CitaController : ControllerBase
    {
        private readonly SistemaReservaCitaContext _context;

        public CitaController(SistemaReservaCitaContext context)
        {
            _context = context;
        }

        // Método para listar todos los servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citum>>> ListarCitas()
        {
            try
            {
                var citas = await _context.Cita.ToListAsync();
                return Ok(citas);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{idPersona}")]
        public async Task<ActionResult<IEnumerable<Citum>>> GetCitasPorPersona(int idPersona)
        {
            var citas = await _context.Cita
                .Where(c => c.IdUsuarioCit == idPersona)
                .ToListAsync();

            if (citas == null)
            {
                return NotFound();
            }

            return citas;
        }

        [HttpPost]
        [Route("Eliminar")]
        public async Task<IActionResult> EliminarCitaYDetalles([FromBody] CitaEliminarRequest request)
        {
            try
            {
                await _context.EliminarCitaYDetalleAsync(request.idCita);
                return Ok("Cita eliminada con éxito.");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }
    }
}
