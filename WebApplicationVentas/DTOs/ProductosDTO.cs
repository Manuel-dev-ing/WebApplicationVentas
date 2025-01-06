namespace WebApplicationVentas.DTOs
{
    public class ProductosDTO
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string CodigoBarras { get; set; }

        public DateTime Fecha { get; set; }
    
    }


}
