using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class RubrosViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre del Rubro es requerido")]
        [Display(Name = "Nombre Rubro")]
        public string Descripcion { get; set; }

        [Display(Name = "Estado del Rubro")]
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}
