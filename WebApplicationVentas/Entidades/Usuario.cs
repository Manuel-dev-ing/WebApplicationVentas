using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public class Usuario
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public string Nombre { get; set; }

    public string Apellidos { get; set; }

    public string Correo { get; set; }

    public string Telefono { get; set; }

    public string Password { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Rol IdRolNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
