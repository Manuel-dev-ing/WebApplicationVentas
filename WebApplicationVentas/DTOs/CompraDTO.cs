namespace WebApplicationVentas.DTOs
{
    public class CompraDTO
    {

        public string IdProveedor { get; set; }
        public string IdAlmacen { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public List<DetalleCompraDTO> productos { get; set; }

    }
}
