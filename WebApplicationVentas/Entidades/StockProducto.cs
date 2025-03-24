using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class StockProducto
{
    public int Id { get; set; }

    public int IdProducto { get; set; }

    public int IdAlmacen { get; set; }
        
    public int StockActual { get; set; }

    public decimal Precio { get; set; }

    public virtual Almacene IdAlmacenNavigation { get; set; }

    public virtual Producto IdProductoNavigation { get; set; }
}
