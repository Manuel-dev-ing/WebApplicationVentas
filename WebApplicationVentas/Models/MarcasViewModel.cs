using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class MarcasViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la Marca es requerido")]
        [Display(Name = "Nombre de la Marca")]
        public string Descripcion { get; set; }
        
        [Display(Name = "Estado de la Marca")]
        public bool EsActivo { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
