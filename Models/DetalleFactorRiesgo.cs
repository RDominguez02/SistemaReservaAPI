using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class DetalleFactorRiesgo
{
    public int IdDetalleFactorRiesgoDetfrg { get; set; }

    public int? IdFactorRiesgoDetfrg { get; set; }

    public string? DescripcionDetfrg { get; set; }

    public TimeSpan? DuracionDetfrg { get; set; }

    public virtual FactorRiesgo? IdFactorRiesgoDetfrgNavigation { get; set; }
}
