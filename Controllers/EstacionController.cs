    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservaAPI.Models;
using SistemaReservaAPI.Server.Models;

namespace SistemaReservaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class
        EstacionController : ControllerBase
    {
        private readonly SistemaReservaCitaContext _context;

        public EstacionController(SistemaReservaCitaContext context)
        {
            _context = context;
        }

        // Método para listar todos los servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estacion>>> ListarEstacion()
        {
            try
            {
                var estaciones = await _context.Estacions.ToListAsync();
                return Ok(estaciones);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("conectarServicio")]
        public async Task<IActionResult> PostEstacionServicio([FromBody] EstacionServicio estacionServicioDto)
        {
            var estacion = await _context.Estacions.FindAsync(estacionServicioDto.IdEstacionEstser);
            var servicio = await _context.Servicios.FindAsync(estacionServicioDto.IdServicioEstser);

            if (estacion == null || servicio == null)
            {
                return NotFound("Lavador o servicio no encontrado.");
            }

            estacion.IdServicioEstsers.Add(servicio);
            await _context.SaveChangesAsync();

            return Ok(); // O devuelve un objeto más informativo si es necesario
        }

        // DELETE: api/EstacionServicio/5/3
        [HttpDelete("{idEstacion}/{idServicio}")]
    
        public async Task<IActionResult> DeleteEstacionServicio(int idEstacion, int idServicio)
        {
            var estacionServicio = await _context.Estacion_Servicio
                .FirstOrDefaultAsync(es => es.IdEstacionEstser == idEstacion && es.IdServicioEstser == idServicio);

            if (estacionServicio == null)
            {
                return NotFound();
            }

            _context.Estacion_Servicio.Remove(estacionServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet]
        [Route("listaEstacionServicio")]
        public async Task<ActionResult<IEnumerable<EstacionServicio>>> GetLavadorServicios()
        {
            return await _context.Estacion_Servicio
                //.Include(ls => ls.Lavador)
                //.Include(ls => ls.Servicio)
                .ToListAsync();
        }
    }
}
