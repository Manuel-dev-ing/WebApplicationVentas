using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class LoginViewModel
    {

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "El Correo electronico es requerido")]
        public string Correo { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "La Contraseña es requerida")]
        public string Password { get; set; }
    
    }
}
