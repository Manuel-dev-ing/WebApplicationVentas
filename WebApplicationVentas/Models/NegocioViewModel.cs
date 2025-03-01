using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationVentas.Models
{
    public class NegocioViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es Requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Telefono es Requerido")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El Correo Electronico es Requerido")]
        [Display(Name = "Correo Electronico")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La Calle es Requerido")]
        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "La Colonia es Requerido")]
        [Display(Name = "Colonia")]
        public string Colonia { get; set; }

        public string ImagenLogotipo { get; set; }

        [Display(Name = "Logotipo del negocio")]
        [Required(ErrorMessage = "El logotipo es requerido")]
        [NotMapped]
        public IFormFile logotipo { get; set; }

    }
}
