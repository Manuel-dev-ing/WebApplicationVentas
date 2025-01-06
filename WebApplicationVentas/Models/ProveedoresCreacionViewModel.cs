using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplicationVentas.Models
{
    public class ProveedoresCreacionViewModel
    {

        public int Id { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un Tipo Documento")]
        [Display(Name = "Tipo Documento")]
        [Required(ErrorMessage = "Debe seleccionar un Tipo Documento")]
        public int IdTipoDocuemto { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un Rubro")]
        [Display(Name = "Rubro")]
        [Required(ErrorMessage = "Debe seleccionar un Rubro")]
        public int IdRubro { get; set; }

        [Display(Name = "Nombre Proveedor")]
        [Required(ErrorMessage = "El nombre del proveedor es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Apeellidos Proveedor")]
        public string Apellidos { get; set; }

        [Display(Name = "Correo Electronico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Numero de Telefono")]
        [Required(ErrorMessage = "el numero de telefono del proveedor es requerido")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Display(Name = "Calle")]
        [Required(ErrorMessage = "La calle requerido")]
        public string Calle { get; set; }

        [Display(Name = "Colonia")]
        [Required(ErrorMessage = "La colonia es requerido")]
        public string Colonia { get; set; }

        [Display(Name = "Codigo Postal / Ciudad")]
        [Required(ErrorMessage = "El codigo postal o ciudad es requerido")]
        public string CodigoPostalCiudad { get; set; }

        public bool EsActivo { get; set; }
        public DateTime Fecha { get; set; }


        public IEnumerable<SelectListItem> tiposRubros { get; set; }
        public IEnumerable<SelectListItem> tiposDocumentos { get; set; }


    }
}
