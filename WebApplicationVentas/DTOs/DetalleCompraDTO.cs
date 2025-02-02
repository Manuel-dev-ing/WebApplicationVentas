namespace WebApplicationVentas.DTOs
{
    public class DetalleCompraDTO
    {
        public int IdProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }

    }
}
