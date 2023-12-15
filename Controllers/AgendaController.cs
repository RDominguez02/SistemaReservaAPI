using Microsoft.AspNetCore.Mvc;
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
                await _context.CrearCitaRapidaAsync(request.IdParametroNumerico, request.ParametroNvarchar);
                return Ok("Cita creada con éxito.");
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
    }
}
