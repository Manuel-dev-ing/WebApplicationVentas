using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioCategorias
    {
        Task<IEnumerable<CategoriaViewModel>> CategoriasActivas();
        int contarElementos();
        int contarElementosInactivos();
        void editarCategoria(Categoria categoria);
        void EliminarCategoria(Categoria categoria);
        Task<bool> existeCategoriaPorId(int id);
        void guardarCategoria(Categoria categoria);
        Task<Categoria> obtenerCategoriaPorId(int id);
        Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasActivas(PaginacionViewModel paginacion);
        Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasInactivas(PaginacionViewModel paginacion);
    }

    public class RepositorioCategorias: IRepositorioCategorias
    {
        private readonly ApplicationDbContext context;

        public RepositorioCategorias(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int contarElementos()
        {
            var resultado = context.Categorias.Where(x => x.EsActivo == true).Count();
            return resultado;
        }

        public int contarElementosInactivos()
        {
            var resultado = context.Categorias.Where(x => x.EsActivo == false).Count();
            return resultado;
        }

        public async Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasActivas(PaginacionViewModel paginacion)
        {
            var entidad = await context.Categorias
                .Where(x => x.EsActivo == true)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new CategoriaViewModel()
                {

                    Id = a.Id,
                    Nombre = a.Descripcion,
                    Esactivo = a.EsActivo,
                    FechaRegistro = a.FechaRegistro

                }).ToListAsync();

            return entidad;
        }

        public async Task<IEnumerable<CategoriaViewModel>> CategoriasActivas()
        {
            var entidad = await context.Categorias
                .Where(x => x.EsActivo == true)
                .Select(a => new CategoriaViewModel()
                {

                    Id = a.Id,
                    Nombre = a.Descripcion,
                    Esactivo = a.EsActivo,
                    FechaRegistro = a.FechaRegistro

                }).ToListAsync();

            return entidad;
        }

        public async Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasInactivas(PaginacionViewModel paginacion)
        {
            var entidad = await context.Categorias
                .Where(x => x.EsActivo == false)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new CategoriaViewModel
            {

                Id = a.Id,
                Nombre = a.Descripcion,
                Esactivo = a.EsActivo,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();

            return entidad;
        }

        public void guardarCategoria(Categoria categoria)
        {

            context.Categorias.Add(categoria);

        }

        public async Task<Categoria> obtenerCategoriaPorId(int id)
        {
            var entidad = await context.Categorias.FirstOrDefaultAsync(x => x.Id == id);

            return entidad;
        }


        public async Task<bool> existeCategoriaPorId(int id)
        {
            var existe = await context.Categorias.AnyAsync(x => x.Id == id);
            return existe;
            //var existe = await context.Autores.AnyAsync(x => x.id == id);

        }

        public void editarCategoria(Categoria categoria)
        {

            context.Categorias.Update(categoria);

        }

        public void EliminarCategoria(Categoria categoria)
        {

            context.Categorias.Update(categoria);

        }




    }
}
