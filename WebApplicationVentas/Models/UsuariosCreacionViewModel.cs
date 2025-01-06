using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationVentas.Models
{
    public class UsuariosCreacionViewModel
    {
        public int Id { get; set; }


        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un Rol")]
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Debe seleccionar un Rol")]
        public int IdRol { get; set; }

        [Display(Name = "Nombre Usuario")]
        [Required(ErrorMessage = "El nombre del usuario es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos del Usuario")]
        [Required(ErrorMessage = "los apellidos del usuario es requerido")]
        public string Apellidos { get; set; }

        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Display(Name = "Numero de Telefono")]
        [Required(ErrorMessage = "el numero de telefono del usuario es requerido")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "la contraseña del usuario es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool EsActivo { get; set; }
        public DateTime Fecha { get; set; }



        public IEnumerable<SelectListItem> tiposRol { get; set; }

    }
}
