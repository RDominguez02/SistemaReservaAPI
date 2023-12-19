using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Models;

public class CitaRapidaRequest
{
    public int IdParametroNumerico { get; set; }
    public required string ParametroNvarchar { get; set; }
}

public class AgendaTuDiaRequest
{
    public int IdParametroNumerico { get; set; }
    public required string ParametroNvarchar { get; set; }

    public required DateTime ParametroDatetime { get; set; }
}

public class CitaEliminarRequest
{
    public int idCita { get; set; }

}