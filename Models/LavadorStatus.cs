using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class LavadorStatus
{
    public int IdLavadorStatusLavsta { get; set; }

    public int? IdLavadorLavsta { get; set; }

    public DateTime? FechaInicioLavsta { get; set; }

    public DateTime? FechaFinLavsta { get; set; }

    public int? StatusLavsta { get; set; }

    public virtual Lavador? IdLavadorLavstaNavigation { get; set; }
}
