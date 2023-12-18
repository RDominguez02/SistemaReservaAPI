using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SistemaReservaAPI.Models;
using SistemaReservaAPI.Server.Models;
using System.Data;

namespace SistemaReservaAPI.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class SessionController : ControllerBase
    {
        private readonly SistemaReservaCitaContext _context;
        public SessionController(SistemaReservaCitaContext context)
        {
            _context = context;
        }

        [HttpPost("IniciarSesion")]
        [Consumes("text/plain")]
        public ActionResult<int> IniciarSesion([FromBody] sesion usuario)
        {
            try
            {
                using (var conn = new SqlConnection("Data Source=SQL5112.site4now.net;Initial Catalog=db_aa1a9f_sistemareservacita;User Id=db_aa1a9f_sistemareservacita_admin;Password=fmdm03997161"))
                {
                    using (var cmd = new SqlCommand("IniciarSesion", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@User", usuario.UserP);
                        cmd.Parameters.AddWithValue("@Clave", usuario.PassP);

                        conn.Open();
                        var result = cmd.ExecuteScalar();

                        if (result != null && (int)result > 0)
                        {
                            // Si el resultado es mayor que 0, significa que el usuario fue encontrado
                            return Ok(result);
                        }
                        else
                        {
                            // Si el resultado es 0, significa que el usuario no fue encontrado o la clave es incorrecta
                            return NotFound("Usuario no encontrado o clave incorrecta.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de la excepción
                return StatusCode(500, "Internal Server Error: " + ex.Message);
            }
        }


    }
}
