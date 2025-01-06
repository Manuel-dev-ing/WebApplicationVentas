using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class EntradaProducto
{
    public int Id { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdAlmacen { get; set; }

    public DateTime? FechaEntradaProducto { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Total { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Almacene IdAlmacenNavigation { get; set; }

    public virtual Proveedore IdProveedorNavigation { get; set; }
}
