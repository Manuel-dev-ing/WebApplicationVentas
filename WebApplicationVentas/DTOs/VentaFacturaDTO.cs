namespace WebApplicationVentas.DTOs
{
    public class VentaFacturaDTO
    {
        public int NumeroVenta { get; set; }
        public string DocumentoCliente { get; set; }
        public string NombreCliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDTO> productos { get; set; }

    }
}
