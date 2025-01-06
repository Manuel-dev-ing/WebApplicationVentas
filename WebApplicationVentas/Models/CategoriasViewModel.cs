using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class CategoriasViewModel
    {
    
        public IEnumerable<CategoriaViewModel> CategoriasActivas { get; set; }

        public IEnumerable<CategoriaViewModel> CategoriasInactivas { get; set; }

    }
}
