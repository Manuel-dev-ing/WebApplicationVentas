using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es requerido")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Display(Name = "Calle")]
        public string Calle { get; set; }

        [Display(Name = "Colonia")]
        public string Colonia { get; set; }

        [Display(Name = "Codigo Postal/Ciuadad")]
        public string CodigoPostalCiudad { get; set; }

        [Display(Name = "Estado del Cliente")]
        public bool Esactivo { get; set; }

    }
}
