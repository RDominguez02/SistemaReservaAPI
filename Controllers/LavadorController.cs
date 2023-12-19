    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservaAPI.Models;
using SistemaReservaAPI.Server.Models;

namespace SistemaReservaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class
        LavadorController : ControllerBase
    {
        private readonly SistemaReservaCitaContext _context;

        public LavadorController(SistemaReservaCitaContext context)
        {
            _context = context;
        }

        // Método para listar todos los servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lavador>>> ListarLavador()
        {
            try
            {
                var lavadores = await _context.Lavadors.ToListAsync();
                return Ok(lavadores);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("conectarServicio")]
        public async Task<IActionResult> PostLavadorServicio([FromBody] LavadorServicio lavadorServicioDto)
        {
            var lavador = await _context.Lavadors.FindAsync(lavadorServicioDto.IdLavadorLavser);
            var servicio = await _context.Servicios.FindAsync(lavadorServicioDto.IdServicioLavser);

            if (lavador == null || servicio == null)
            {
                return NotFound("Lavador o servicio no encontrado.");
            }

            lavador.IdServicioLavsers.Add(servicio);
            await _context.SaveChangesAsync();

            return Ok(); // O devuelve un objeto más informativo si es necesario
        }


        [HttpGet]
        [Route("listaLavadorServicio")]
        public async Task<ActionResult<IEnumerable<LavadorServicio>>> GetLavadorServicios()
        {
            return await _context.Lavador_Servicio
                //.Include(ls => ls.Lavador)
                //.Include(ls => ls.Servicio)
                .ToListAsync();
        }
    }
}
