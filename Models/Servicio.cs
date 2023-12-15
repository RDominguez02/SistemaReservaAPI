using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class Servicio
{
    public int IdServicioSer { get; set; }

    public string? DescripcionSer { get; set; }

    public decimal? CostoSer { get; set; }

    public TimeSpan? DuracionSer { get; set; }

    public virtual ICollection<DetalleCitum> DetalleCita { get; set; } = new List<DetalleCitum>();

    public virtual ICollection<Estacion> IdEstacionEstsers { get; set; } = new List<Estacion>();

    public virtual ICollection<Lavador> IdLavadorLavsers { get; set; } = new List<Lavador>();
}
