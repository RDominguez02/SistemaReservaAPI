using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class EstacionStatus
{
    public int IdEstacionStatusEststa { get; set; }

    public int? IdEsacionEststa { get; set; }

    public DateTime? FechaInicioEststa { get; set; }

    public DateTime? FechaFinEststa { get; set; }

    public int? StatusEststa { get; set; }

    public virtual Estacion? IdEsacionEststaNavigation { get; set; }
}
