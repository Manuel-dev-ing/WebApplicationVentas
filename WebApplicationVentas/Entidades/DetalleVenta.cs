using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public class DetalleVenta
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public string DescripcionProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal Total { get; set; }

    public Venta IdVentaNavigation { get; set; }
}
