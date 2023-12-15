using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class Usuario
{
    public int IdUsuarioUsu { get; set; }

    public string? NombreUsu { get; set; }

    public int? IdRolUsu { get; set; }

    public string? NombreUsuarioUsu { get; set; }

    public string? ClaveUsu { get; set; }

    public virtual ICollection<Citum> Cita { get; set; } = new List<Citum>();

    public virtual Rol? IdRolUsuNavigation { get; set; }
}
