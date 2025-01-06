using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class TipoDocumentoVentum
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
