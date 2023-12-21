using Microsoft.EntityFrameworkCore;

namespace SistemaReservaAPI.Models
{
    public class InfoCita
    {
            public int idCita_cit { get; set; }
            public string Descripcion_est { get; set; }
            public string Nombre_lav { get; set; }
            public DateTime Fecha_cit { get; set; }
            public TimeSpan Duracion_cit { get; set; }
            public DateTime FechaFin_cit { get; set; }
    }
 }

