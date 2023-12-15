using System;
using System.Collections.Generic;

namespace SistemaReservaAPI.Server.Models;

public partial class Rol
{
    public int IdRolRol { get; set; }

    public string? DescripcionRol { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
