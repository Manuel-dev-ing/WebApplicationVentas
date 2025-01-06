namespace WebApplicationVentas.Models
{
    public class ProductosListadoViewModel
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha { get; set; }

    }
}
