using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class Estacion
{
    public int IdEstacionEst { get; set; }

    public string? DescripcionEst { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<EstacionStatus> EstacionStatuses { get; set; } = new List<EstacionStatus>();

    public virtual ICollection<Servicio> IdServicioEstsers { get; set; } = new List<Servicio>();
}
