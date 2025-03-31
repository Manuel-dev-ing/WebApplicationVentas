using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioRol
    {
        void Actualizar(Rol rol);
        int contarElementos();
        int contarElementosInactivos();
        Task<bool> existeRol(int id);
        void Guardar(Rol rol);
        Task<IEnumerable<RolViewModel>> Listadorol();
        Task<Rol> obtenerPorId(int id);
        Task<Rol> obtenerRolPorNombre(string nombreRol);
        Task<IEnumerable<RolViewModel>> rolActivo(PaginacionViewModel paginacion);
        Task<IEnumerable<RolViewModel>> rolInactivo(PaginacionViewModel paginacion);
    }


    public class RepositorioRol: IRepositorioRol
    {
        private readonly ApplicationDbContext context;

        public RepositorioRol(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int contarElementos()
        {
            var resultado = context.Rols.Where(x => x.EsActivo == true).Count();
            return resultado;
        }

        public int contarElementosInactivos()
        {
            var resultado = context.Rols.Where(x => x.EsActivo == false).Count();
            return resultado;
        }

        public async Task<IEnumerable<RolViewModel>> Listadorol()
        {
            var entidad = await context.Rols
                .Where(x => x.EsActivo == true)
                .Select(a => new RolViewModel()
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.FechaRegistro

                }).ToListAsync();

            return entidad;
        }


        public async Task<IEnumerable<RolViewModel>> rolActivo(PaginacionViewModel paginacion)
        {
            var entidad = await context.Rols
                .Where(x => x.EsActivo == true)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new RolViewModel()
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.FechaRegistro

                }).ToListAsync();

            return entidad;
        }

        public async Task<IEnumerable<RolViewModel>> rolInactivo(PaginacionViewModel paginacion)
        {
            var entidad = await context.Rols
                .Where(x => x.EsActivo == false)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new RolViewModel()
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.FechaRegistro

                }).ToListAsync();

            return entidad;
        }


        public void Guardar(Rol rol)
        {
            context.Rols.Add(rol);
        }
        public void Actualizar(Rol rol)
        {
            context.Rols.Update(rol);
        }

        public async Task<bool> existeRol(int id)
        {
            var entidad = await context.Rols.AnyAsync(x => x.Id == id);
            return entidad;

        }

        public async Task<Rol> obtenerPorId(int id)
        {
            var entidad = await context.Rols.FirstOrDefaultAsync(x => x.Id == id);

            return entidad;

        }

        public async Task<Rol> obtenerRolPorNombre(string nombreRol)
        {

            var entidad = await context.Rols.Where(x => x.Descripcion == nombreRol).FirstOrDefaultAsync();
            return entidad;
        }





    }
}
