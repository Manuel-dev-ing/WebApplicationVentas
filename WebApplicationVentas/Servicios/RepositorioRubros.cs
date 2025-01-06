using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioRubros
    {
        void editarRubro(Rubro rubro);
        Task<bool> existeRubro(int id);
        void guardarRubro(Rubro rubro);
        Task<Rubro> obtenerRubroPorId(int id);
        Task<IEnumerable<RubrosViewModel>> rubrosActvos();
        Task<IEnumerable<RubrosViewModel>> rubrosInactvos();
    }

    public class RepositorioRubros: IRepositorioRubros
    {
        private readonly ApplicationDbContext context;

        public RepositorioRubros(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<RubrosViewModel>> rubrosActvos()
        {
            var rubros = await context.Rubros.Where(x => x.EsActivo == true).Select(a => new RubrosViewModel()
            {
                Id = a.Id,
                Descripcion = a.Descripcion,
                EsActivo = a.EsActivo,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();

            return rubros;
        }

        public async Task<IEnumerable<RubrosViewModel>> rubrosInactvos()
        {
            var rubros = await context.Rubros.Where(x => x.EsActivo == false).Select(a => new RubrosViewModel()
            {
                Id = a.Id,
                Descripcion = a.Descripcion,
                EsActivo = a.EsActivo,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();

            return rubros;
        }

        public async Task<Rubro> obtenerRubroPorId(int id)
        {
            var rubro = await context.Rubros.FirstOrDefaultAsync(x => x.Id == id);
            return rubro;
        }

        public async Task<bool> existeRubro(int id)
        {
            var rubro = await context.Rubros.AnyAsync(x => x.Id == id);

            return rubro;
        }

        public void guardarRubro(Rubro rubro)
        {
            context.Rubros.Add(rubro);
        }

        public void editarRubro(Rubro rubro)
        {
            context.Rubros.Update(rubro);
        }




    }
}
