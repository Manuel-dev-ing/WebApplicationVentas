namespace WebApplicationVentas.Models
{
    public class UsuariosModel
    {


        public IEnumerable<UsuariosViewModel> usuariosActivos { get; set; }
        public IEnumerable<UsuariosViewModel> usuariosInactivos { get; set; }


    }
}
