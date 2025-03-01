using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class Venta
{
    public int Id { get; set; }

    public int IdTipoDocumentoVenta { get; set; }

    public int IdUsuario { get; set; }

    public int IdCliente { get; set; }

    public int NumeroVenta { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Total { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Cliente IdClienteNavigation { get; set; }

    public virtual TipoDocumentoVentum IdTipoDocumentoVentaNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}
