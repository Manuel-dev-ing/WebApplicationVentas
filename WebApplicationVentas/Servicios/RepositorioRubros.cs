using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioRubros
    {
        int contarElementos();
        int contarElementosInactivos();
        void editarRubro(Rubro rubro);
        Task<bool> existeRubro(int id);
        void guardarRubro(Rubro rubro);
        Task<IEnumerable<RubrosViewModel>> listadoActvos();
        Task<Rubro> obtenerRubroPorId(int id);
        Task<IEnumerable<RubrosViewModel>> rubrosActvos(PaginacionViewModel paginacion);
        Task<IEnumerable<RubrosViewModel>> rubrosInactvos(PaginacionViewModel paginacion);
    }

    public class RepositorioRubros: IRepositorioRubros
    {
        private readonly ApplicationDbContext context;

        public RepositorioRubros(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int contarElementos()
        {
            var resultado = context.Rubros.Where(x => x.EsActivo == true).Count();
            return resultado;
        }
        public int contarElementosInactivos()
        {
            var resultado = context.Rubros.Where(x => x.EsActivo == false).Count();
            return resultado;
        }

        public async Task<IEnumerable<RubrosViewModel>> listadoActvos()
        {
            var rubros = await context.Rubros
                .Where(x => x.EsActivo == true)
                .Select(a => new RubrosViewModel()
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    EsActivo = a.EsActivo,
                    FechaRegistro = a.FechaRegistro

                }).ToListAsync();

            return rubros;
        }

        public async Task<IEnumerable<RubrosViewModel>> rubrosActvos(PaginacionViewModel paginacion)
        {
            var rubros = await context.Rubros
                .Where(x => x.EsActivo == true)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new RubrosViewModel()
            {
                Id = a.Id,
                Descripcion = a.Descripcion,
                EsActivo = a.EsActivo,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();

            return rubros;
        }

        public async Task<IEnumerable<RubrosViewModel>> rubrosInactvos(PaginacionViewModel paginacion)
        {
            var rubros = await context.Rubros
                .Where(x => x.EsActivo == false)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new RubrosViewModel()
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
