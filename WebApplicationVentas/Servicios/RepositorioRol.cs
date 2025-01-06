using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioRol
    {
        void Actualizar(Rol rol);
        Task<bool> existeRol(int id);
        void Guardar(Rol rol);
        Task<Rol> obtenerPorId(int id);
        Task<IEnumerable<RolViewModel>> rolActivo();
        Task<IEnumerable<RolViewModel>> rolInactivo();
    }


    public class RepositorioRol: IRepositorioRol
    {
        private readonly ApplicationDbContext context;

        public RepositorioRol(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<RolViewModel>> rolActivo()
        {
            var entidad = await context.Rols.Where(x => x.EsActivo == true).Select(a => new RolViewModel()
            {
                Id = a.Id,
                Descripcion = a.Descripcion,
                Fecha = a.FechaRegistro
            }).ToListAsync();

            return entidad;
        }

        public async Task<IEnumerable<RolViewModel>> rolInactivo()
        {
            var entidad = await context.Rols.Where(x => x.EsActivo == false).Select(a => new RolViewModel()
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

    }
}
