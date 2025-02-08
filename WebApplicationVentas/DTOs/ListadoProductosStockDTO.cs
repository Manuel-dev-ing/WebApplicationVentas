namespace WebApplicationVentas.DTOs
{
    public class ListadoProductosStockDTO
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int StockActual { get; set; }
        public decimal Precio { get; set; }

        public List<ProductosListadoDTO> ListadoProductos { get; set; }

    }
}
