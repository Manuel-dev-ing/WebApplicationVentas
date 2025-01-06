using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace WebApplicationVentas.Models
{
    public class ProductosViewModel
    {
        public int Id { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un Autor")]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Debe seleccionar una Marca")]
        public int IdMarca { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar una Categoria")]
        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "Debe seleccionar una Categoria")]
        public int IdCategoria { get; set; }

        [Display(Name = "Codigo de Barras")]
        [Required(ErrorMessage = "El codigo de barras es requerido")]
        public string CodigoBarras { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "La descripcion del producto es requerido")]
        public string Descripcion { get; set; }

        [Display(Name = "Stock Minimo")]
        [Required(ErrorMessage = "El Stock Minimo es requerido")]
        public int StockMinimo { get; set; }

        [Display(Name = "Stock Maximo")]
        [Required(ErrorMessage = "El Stock Maximo es requrido")]
        public int StockMaximo { get; set; }

        public string ImagenProducto { get; set; }

        [Display(Name = "Imagen Producto")]
        [Required(ErrorMessage = "La imagen del producto es requerido")]
        [NotMapped]
        public IFormFile ImagenArchivo { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El precio del producto es requerido")]
        public decimal Precio { get; set; }

        public bool EsActivo { get; set; }

        public DateTime FechaRegistro { get; set; }

    }
}
