using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class CreacionTiposDocumentosViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Nombre del Tipo Documento")]
        [Required(ErrorMessage = "El Nombre del Tipo Documento es Requerido")]
        public string Descripcion { get; set; }

        public bool Es_Activo { get; set; }
        public DateTime Fecha { get; set; }

    }
}
