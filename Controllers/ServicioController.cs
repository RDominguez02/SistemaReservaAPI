using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaReservaAPI.Models;
using SistemaReservaAPI.Server.Models;

namespace SistemaReservaAPI.Controllers
{
    public class ServicioController : Controller
    {
        private readonly SistemaReservaCitaContext _context;

        public ServicioController(SistemaReservaCitaContext context)
        {
            _context = context;
        }

        // Método para listar todos los servicios
        [HttpGet]
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
