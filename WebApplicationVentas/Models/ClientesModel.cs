namespace WebApplicationVentas.Models
{
    public class ClientesModel
    {

        public IEnumerable<ClienteViewModel> clientesActivos { get; set; }
        public IEnumerable<ClienteViewModel> clientesInactivos { get; set; }


    }
}
