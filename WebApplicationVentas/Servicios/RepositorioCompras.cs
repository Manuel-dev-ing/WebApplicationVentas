using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using WebApplicationVentas.DTOs;
using WebApplicationVentas.Entidades;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioCompras
    {
        void guardar(EntradaProducto entradaProducto);
        Task<List<AlmacenesDTO>> listadoAlmacenes();
        Task<List<ProveedoresDTO>> listadoProveedores();
    }
    public class RepositorioCompras: IRepositorioCompras
    {
        private readonly ApplicationDbContext context;

        public RepositorioCompras(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<AlmacenesDTO>> listadoAlmacenes()
        {

            var entidad = await context.Almacenes.Where(x => x.EsActivo == true).Select(a => new AlmacenesDTO()
            {
                Id = a.Id,
                Descripcion = a.Descripcion

            }).ToListAsync();

            return entidad;
        }

        public async Task<List<ProveedoresDTO>> listadoProveedores()
        {

            var entidad = await context.Proveedores.Where(x => x.EsActivo == true).Select(a => new ProveedoresDTO()
            {
                Id = a.Id,
                Nombre = a.Nombre

            }).ToListAsync();

            return entidad;
        }

        public void guardar(EntradaProducto entradaProducto)
        {
            context.EntradaProductos.AddAsync(entradaProducto);
        }

    }
}
