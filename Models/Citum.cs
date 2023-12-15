using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class Citum
{
    public int IdCitaCit { get; set; }

    public int? IdUsuarioCit { get; set; }

    public int? IdEstacionCit { get; set; }

    public int? IdLavadorCit { get; set; }

    public DateTime? FechaCit { get; set; }

    public TimeSpan? DuracionCit { get; set; }

    public DateTime? FechaFinCit { get; set; }

    public byte? EstadoCit { get; set; }

    public virtual ICollection<DetalleCitum> DetalleCita { get; set; } = new List<DetalleCitum>();

    public virtual Estacion? IdEstacionCitNavigation { get; set; }

    public virtual Lavador? IdLavadorCitNavigation { get; set; }

    public virtual Usuario? IdUsuarioCitNavigation { get; set; }
}
