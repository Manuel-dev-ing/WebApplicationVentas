namespace WebApplicationVentas.Models
{
    public class StockProductosViewModel
    {
        public int Id { get; set; }
        public string Producto { get; set; }
        public string Almacen { get; set; }
        public int StockActual { get; set; }
        public decimal Precio { get; set; }
    }
}
