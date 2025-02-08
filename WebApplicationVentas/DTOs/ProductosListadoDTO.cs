namespace WebApplicationVentas.DTOs
{
    public class ProductosListadoDTO
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public int StockMinimo { get; set; }
        public int StockMaximo { get; set; }

    }
}
