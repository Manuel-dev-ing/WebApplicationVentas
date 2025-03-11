using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{

    public interface IRepositorioProveedores
    {
        void actualizar(Proveedore proveedore);
        int contarElementos();
        int contarElementosInactivos();
        Task<bool> existeProveedor(int id);
        void guardar(Proveedore proveedor);
        Task<Proveedore> obtenerProveedorPorId(int id);
        Task<IEnumerable<ProvedoresViewModel>> proveedoresActivos(PaginacionViewModel paginacion);
        Task<IEnumerable<ProvedoresViewModel>> proveedoresInactivos(PaginacionViewModel paginacion);
    }

    public class RepositorioProveedores: IRepositorioProveedores
    {
        private readonly ApplicationDbContext context;

        public RepositorioProveedores(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int contarElementos()
        {
            var resultado = context.Proveedores.Where(x => x.EsActivo == true).Count();
            return resultado;
        }

        public int contarElementosInactivos()
        {
            var resultado = context.Proveedores.Where(x => x.EsActivo == false).Count();
            return resultado;
        }

        public async Task<IEnumerable<ProvedoresViewModel>> proveedoresActivos(PaginacionViewModel paginacion)
        {
            var entidad = await context.Proveedores
                .Include(x => x.IdRubroNavigation)
                .Where(x => x.EsActivo == true)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new ProvedoresViewModel()
                {
                    Id = a.Id,
                    Rubros = a.IdRubroNavigation.Descripcion,
                    Nombre = a.Nombre,
                    Email = a.Email,
                    Telefono = a.Telefono
                }).ToListAsync();

            return entidad;
        }

        public async Task<IEnumerable<ProvedoresViewModel>> proveedoresInactivos(PaginacionViewModel paginacion)
        {
            var entidad = await context.Proveedores
                .Include(x => x.IdRubroNavigation)
                .Where(x => x.EsActivo == false)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new ProvedoresViewModel()
                {
                    
                    Id = a.Id,
                    Rubros = a.IdRubroNavigation.Descripcion,
                    Nombre = a.Nombre,
                    Email = a.Email,
                    Telefono = a.Telefono

                }).ToListAsync();

            return entidad;
        }

        public void guardar(Proveedore proveedor)
        {
            context.Proveedores.Add(proveedor);
        }

        public void actualizar(Proveedore proveedore)
        {

            context.Proveedores.Update(proveedore);

        }

        public async Task<bool> existeProveedor(int id)
        {

            var entidad = await context.Proveedores.AnyAsync(x => x.Id == id);
            return entidad;

        }

        public async Task<Proveedore> obtenerProveedorPorId(int id)
        {
            var entidad = await context.Proveedores.FirstOrDefaultAsync(x => x.Id == id);
            return entidad;
        }




    }
}
