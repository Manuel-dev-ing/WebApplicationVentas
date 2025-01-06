using System.ComponentModel.DataAnnotations;

namespace WebApplicationVentas.Models
{
    public class AlmacenesModel
    {
        public IEnumerable<AlmacenViewModel> AlmaceesActivos { get; set; }
        public IEnumerable<AlmacenViewModel> AlmaceesInactivos { get; set; }


    }
}
