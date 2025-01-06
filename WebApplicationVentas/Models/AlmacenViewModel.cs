using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class AlmacenViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del Almacen")]
        [Required(ErrorMessage = "El nombre del almacen es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Estado del Almacen")]
        public bool Esactivo { get; set; }

        public DateTime FechaRegistro { get; set; }

    }


}
