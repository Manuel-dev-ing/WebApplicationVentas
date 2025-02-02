using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class EditarStockProductosViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Stock es requerido")]
        [Display(Name = "Escribe el stock para el producto")]
        public int Stock { get; set; }
        public string Almacen { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProducto { get; set; }
        public string Imagen { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
    }
}
