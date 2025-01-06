using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class Rubro
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Proveedore> Proveedores { get; set; } = new List<Proveedore>();
}
