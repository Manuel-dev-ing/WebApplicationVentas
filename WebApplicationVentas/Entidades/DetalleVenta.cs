﻿using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class DetalleVenta
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public decimal Precio { get; set; }

    public decimal Total { get; set; }

    public virtual Producto IdProductoNavigation { get; set; }

    public virtual Venta IdVentaNavigation { get; set; }
}
