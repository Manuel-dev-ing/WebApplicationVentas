using WebApplicationVentas.DTOs;

namespace WebApplicationVentas.Models
{
    public class VentaFacturaViewModel
    {

        public int NumeroVenta { get; set; }
        public int IdTipoDocumento { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public string logotipo { get; set; }
        public string NombreNegocio { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public List<DetalleVentaFactura> productos { get; set; }
    }
}
