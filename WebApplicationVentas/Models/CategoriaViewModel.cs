using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre de la Categoria")]
        [Required(ErrorMessage = "El nombre de la categoria es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Estado de la Categoria")]
        public bool Esactivo { get; set; }

        public DateTime FechaRegistro { get; set; }

    }
}
