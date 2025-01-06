using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationVentas.Models
{
    public class ProductosCreacionViewModel: ProductosViewModel
    {

        public IEnumerable<SelectListItem> tiposMarcas { get; set; }
        public IEnumerable<SelectListItem> tiposCategorias { get; set; }

    }

}
