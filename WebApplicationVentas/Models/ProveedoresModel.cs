namespace WebApplicationVentas.Models
{
    public class ProveedoresModel
    {
        public IEnumerable<ProvedoresViewModel> proveedoresActivos { get; set; }
        public IEnumerable<ProvedoresViewModel> proveedoresInactivos { get; set; }

    }
}
