using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class Horario
{
    public int IdHorarioHor { get; set; }

    public int? IdLavadorHor { get; set; }

    public TimeSpan? HoraInicioHor { get; set; }

    public TimeSpan? HoraFinalHor { get; set; }

    public bool? EsActivo { get; set; }

    public virtual Lavador? IdLavadorHorNavigation { get; set; }
}
