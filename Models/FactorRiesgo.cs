using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class FactorRiesgo
{
    public int IdFactorRiesgoFrg { get; set; }

    public string? DescripcionFrg { get; set; }

    public TimeSpan? DuracionTotalFrg { get; set; }

    public virtual ICollection<DetalleFactorRiesgo> DetalleFactorRiesgos { get; set; } = new List<DetalleFactorRiesgo>();
}
