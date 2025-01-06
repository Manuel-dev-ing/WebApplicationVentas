using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class RolCreacionViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Nombre Rol")]
        [Required(ErrorMessage = "El nombre del rol es requerido")]
        public string Descripcion { get; set; }

        public bool EsActivo { get; set; }
        public DateTime Fecha { get; set; }

    }
}
