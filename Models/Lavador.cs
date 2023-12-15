using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class Lavador
{
    public int IdLavadorLav { get; set; }

    public string? NombreLav { get; set; }

    public bool? EsActivo { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual ICollection<Horario> Horarios { get; set; } = new List<Horario>();

    public virtual ICollection<LavadorStatus> LavadorStatuses { get; set; } = new List<LavadorStatus>();

    public virtual ICollection<Servicio> IdServicioLavsers { get; set; } = new List<Servicio>();
}
