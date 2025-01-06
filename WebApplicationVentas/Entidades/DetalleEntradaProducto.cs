using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class DetalleEntradaProducto
{
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }

    public virtual Producto IdProductoNavigation { get; set; }
}
