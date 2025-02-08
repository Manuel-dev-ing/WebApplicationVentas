namespace WebApplicationVentas.DTOs
{
    public class VentaDetalleDTO
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Total { get; set; }

    }
}
