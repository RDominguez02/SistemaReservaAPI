    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservaAPI.Models;
using SistemaReservaAPI.Server.Models;

namespace SistemaReservaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class
        AgendaController : ControllerBase
    {
        private readonly SistemaReservaCitaContext _context;

        public AgendaController(SistemaReservaCitaContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Rapida")]
        public async Task<IActionResult> CrearCitaRapida([FromBody] CitaRapidaRequest request)
        {
            try
            {
                var mensaje = await _context.CrearCitaRapidaAsync(request.IdParametroNumerico, request.ParametroNvarchar);
                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Agenda")]
        public async Task<IActionResult> CrearAgendatuDia([FromBody] AgendaTuDiaRequest request)
        {
            try
            {
                await _context.CrearAgendatuDiaAsync(request.IdParametroNumerico, request.ParametroNvarchar, request.ParametroDatetime);
                return Ok("Cita creada con éxito.");
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }


        // Método para listar todos los servicios
        [HttpGet]
        [Route("Servcios")]
        public async Task<ActionResult<IEnumerable<Servicio>>> ListarServicios()
        {
            try
            {
                var servicios = await _context.Servicios.ToListAsync();
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, ex.Message);
            }
        }
    }
}
