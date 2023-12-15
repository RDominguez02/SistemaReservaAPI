using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class DetalleCitum
{
    public int IdDetalleCitaDct { get; set; }

    public int IdCitaDct { get; set; }

    public int? IdServicioDct { get; set; }

    public TimeSpan? DuracionDct { get; set; }

    public virtual Citum IdCitaDctNavigation { get; set; } = null!;

    public virtual Servicio? IdServicioDctNavigation { get; set; }
}
