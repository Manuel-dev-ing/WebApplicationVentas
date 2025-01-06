using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioAlmacenes
    {
        void actualizar(Almacene almacene);
        void crear(Almacene almacen);
        void eliminar(Almacene almacene);
        Task<IEnumerable<AlmacenViewModel>> obteneAlmacenesInactivos();
        Task<IEnumerable<AlmacenViewModel>> obtenerAlmacenes();
        Task<IEnumerable<AlmacenViewModel>> obtenerAlmacenesActivos();
        Task<Almacene> obtenerAlmacenPorId(int id);
    }


    public class RepositoriosAlmacenes: IRepositorioAlmacenes
    {
        private readonly ApplicationDbContext context;

        public RepositoriosAlmacenes(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<AlmacenViewModel>> obtenerAlmacenesActivos()
        {
            var resultado = await context.Almacenes.Where(x => x.EsActivo == true).Select(a => new AlmacenViewModel
            {
                Id = a.Id,
                Nombre = a.Descripcion,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();
            
            

            return resultado;
        }

        public async Task<IEnumerable<AlmacenViewModel>> obteneAlmacenesInactivos()
        {
            var resultado = await context.Almacenes.Where(x => x.EsActivo == false).Select(a => new AlmacenViewModel
            {
                Id = a.Id,
                Nombre = a.Descripcion,
                Esactivo = a.EsActivo,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();

            return resultado;
        }


        public async Task<IEnumerable<AlmacenViewModel>> obtenerAlmacenes()
        {
            var resultado = await context.Almacenes.Select(x => new AlmacenViewModel
            {   
                Id = x.Id,
                Nombre = x.Descripcion,
                FechaRegistro = x.FechaRegistro

            }).ToListAsync();
            return resultado;
        }

        public void crear(Almacene almacen)
        {
            context.Almacenes.Add(almacen);
        }

        public async Task<Almacene> obtenerAlmacenPorId(int id)
        {

            var resultado = await context.Almacenes.FirstOrDefaultAsync(x => x.Id == id);

            return resultado;

        }

        public void actualizar(Almacene almacene)
        {
            context.Almacenes.Update(almacene);
        }

        public void eliminar(Almacene almacene)
        {
            context.Almacenes.Update(almacene);
        }

    }
}
