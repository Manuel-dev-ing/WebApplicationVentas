using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class Producto
{
    public int Id { get; set; }

    public int IdMarca { get; set; }

    public int IdCategoria { get; set; }

    public string CodigoBarras { get; set; }

    public string Descripcion { get; set; }

    public int StockMinimo { get; set; }

    public int StockMaximo { get; set; }

    public string Imagen { get; set; }

    public decimal Precio { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<DetalleEntradaProducto> DetalleEntradaProductos { get; set; } = new List<DetalleEntradaProducto>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Categoria IdCategoriaNavigation { get; set; }

    public virtual Marca IdMarcaNavigation { get; set; }

    public virtual ICollection<StockProducto> StockProductos { get; set; } = new List<StockProducto>();
}
