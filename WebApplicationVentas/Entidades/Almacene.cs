using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class Almacene
{
    public int Id { get; set; }

    public string Descripcion { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<EntradaProducto> EntradaProductos { get; set; } = new List<EntradaProducto>();

    public virtual ICollection<StockProducto> StockProductos { get; set; } = new List<StockProducto>();
}
