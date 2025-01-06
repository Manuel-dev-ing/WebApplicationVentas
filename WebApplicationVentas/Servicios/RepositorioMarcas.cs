using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioMarcas
    {
        void editar(Marca marca);
        void eliminar(Marca marca);
        Task<bool> existeMarca(int id);
        void guardar(Marca marca);
        Task<IEnumerable<MarcasViewModel>> marcasActivas();
        Task<IEnumerable<MarcasViewModel>> marcasInactivas();
        Task<Marca> obtenerPorId(int id);
    }


    public class RepositorioMarcas: IRepositorioMarcas
    {
        private readonly ApplicationDbContext context;

        public RepositorioMarcas(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MarcasViewModel>> marcasActivas()
        {
            var marca = await context.Marcas.Where(x => x.EsActivo == true).Select(a => new MarcasViewModel()
            {
                Id = a.Id,
                Descripcion = a.Descripcion,
                EsActivo = a.EsActivo,
                FechaCreacion = a.FechaRegistro
            }).ToListAsync();

            return marca;
        }
        public async Task<IEnumerable<MarcasViewModel>> marcasInactivas()
        {
            var marca = await context.Marcas.Where(x => x.EsActivo == false).Select(a => new MarcasViewModel()
            {
                Id = a.Id,
                Descripcion = a.Descripcion,
                EsActivo = a.EsActivo,
                FechaCreacion = a.FechaRegistro
            }).ToListAsync();

            return marca;
        }

        public void guardar(Marca marca)
        {
            context.Marcas.Add(marca);
        
        }
        public void editar(Marca marca)
        {
            context.Marcas.Update(marca);

        }

        public void eliminar(Marca marca)
        {
            context.Marcas.Update(marca);

        }


        public async Task<Marca> obtenerPorId(int id)
        {
            var marca = await context.Marcas.FirstOrDefaultAsync(x => x.Id == id);
            return marca;
        }

        public async Task<bool> existeMarca(int id)
        {
            var existeMarca = await context.Marcas.AnyAsync(x => x.Id == id);
            return existeMarca;
        }


    }
}
