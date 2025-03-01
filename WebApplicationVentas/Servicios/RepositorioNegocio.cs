using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioNegocio
    {
        void actualizar(Negocio negocio);
        void guardar(Negocio negocio);
        Task<NegocioCreacionViewModel> obtener();
    }

    public class RepositorioNegocio: IRepositorioNegocio
    {
        private readonly ApplicationDbContext context;

        public RepositorioNegocio(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<NegocioCreacionViewModel> obtener()
        {
            var entidad = await context.Negocios.Select(x => new NegocioCreacionViewModel()
            {
                Id = x.Id,
                Nombre = x.Nombre,
                Telefono = x.Telefono,
                Correo = x.Correo,
                Calle = x.Calle,
                Colonia = x.Colonia,
                ImagenLogotipo = x.Logotipo

            }).FirstOrDefaultAsync();

            return entidad;
        
        }

        public void guardar(Negocio negocio)
        {
            context.Negocios.Add(negocio);
        }

        public void actualizar(Negocio negocio)
        {
            context.Negocios.Update(negocio);
        }

    }
}
