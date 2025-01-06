using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioCategorias
    {
        void editarCategoria(Categoria categoria);
        void EliminarCategoria(Categoria categoria);
        Task<bool> existeCategoriaPorId(int id);
        void guardarCategoria(Categoria categoria);
        Task<Categoria> obtenerCategoriaPorId(int id);
        Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasActivas();
        Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasInactivas();
    }

    public class RepositorioCategorias: IRepositorioCategorias
    {
        private readonly ApplicationDbContext context;

        public RepositorioCategorias(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasActivas()
        {
            var entidad = await context.Categorias.Where(x => x.EsActivo == true).Select(a => new CategoriaViewModel
            {

                Id = a.Id,
                Nombre = a.Descripcion,
                Esactivo = a.EsActivo,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();

            return entidad;
        }

        public async Task<IEnumerable<CategoriaViewModel>> obtenerCategoriasInactivas()
        {
            var entidad = await context.Categorias.Where(x => x.EsActivo == false).Select(a => new CategoriaViewModel
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
