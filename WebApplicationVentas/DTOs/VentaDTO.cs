namespace WebApplicationVentas.DTOs
{
    public class VentaDTO
    {
        public string idDocVenta { get; set; }
        public string NombreCliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public List<DetalleVentaDTO> productos { get; set; }

    }
}
