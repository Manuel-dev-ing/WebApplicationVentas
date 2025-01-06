using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public class Venta
{
    public int Id { get; set; }

    public int IdTipoDocumentoVenta { get; set; }

    public int IdUsuario { get; set; }

    public string NombreCliente { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Total { get; set; }

    public DateTime FechaRegistro { get; set; }

    public List<DetalleVenta> DetalleVentas { get; set; }

    public TipoDocumentoVentum IdTipoDocumentoVentaNavigation { get; set; }

    public Usuario IdUsuarioNavigation { get; set; }
}
