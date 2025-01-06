namespace WebApplicationVentas.Models
{
    public class ProductosModel
    {

        public IEnumerable<ProductosListadoViewModel> ProductosActivos { get; set; }
        public IEnumerable<ProductosListadoViewModel> ProductosInactivos { get; set; }


    }
}
