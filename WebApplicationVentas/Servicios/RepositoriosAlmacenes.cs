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
        int contarElementos();
        int contarElementosInactivos();
        void crear(Almacene almacen);
        void eliminar(Almacene almacene);
        Task<IEnumerable<AlmacenViewModel>> obteneAlmacenesInactivos(PaginacionViewModel paginacion);
        Task<IEnumerable<AlmacenViewModel>> obtenerAlmacenes();
        Task<IEnumerable<AlmacenViewModel>> obtenerAlmacenesActivos(PaginacionViewModel paginacion);
        Task<Almacene> obtenerAlmacenPorId(int id);
    }


    public class RepositoriosAlmacenes: IRepositorioAlmacenes
    {
        private readonly ApplicationDbContext context;

        public RepositoriosAlmacenes(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int contarElementos()
        {
            var resultado = context.Almacenes.Where(x => x.EsActivo == true).Count();
            return resultado;
        }

        public int contarElementosInactivos()
        {
            var resultado = context.Almacenes.Where(x => x.EsActivo == false).Count();
            return resultado;
        }

        public async Task<IEnumerable<AlmacenViewModel>> obtenerAlmacenesActivos(PaginacionViewModel paginacion)
        {
            var resultado = await context.Almacenes
                .Where(x => x.EsActivo == true)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new AlmacenViewModel(){

                    Id = a.Id,
                    Nombre = a.Descripcion,
                    FechaRegistro = a.FechaRegistro

                }).ToListAsync();
            
            

            return resultado;
        }

        public async Task<IEnumerable<AlmacenViewModel>> obteneAlmacenesInactivos(PaginacionViewModel paginacion)
        {
            var resultado = await context.Almacenes
                .Where(x => x.EsActivo == false)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new AlmacenViewModel
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
