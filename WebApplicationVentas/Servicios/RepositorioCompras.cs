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
        Task<List<CompraDetalleDTO>> listadoDetalleCompra(int id);
        Task<List<ProveedoresDTO>> listadoProveedores();
        Task<List<ComprasListadoDTO>> obtenerComprasPorFecha(string fechaInicio, string fechaFin);
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

        public async Task<List<ComprasListadoDTO>> obtenerComprasPorFecha(string fechaInicio, string fechaFin)
        {

            var dateStart = Convert.ToDateTime(fechaInicio);
            var dateEnd = Convert.ToDateTime(fechaFin);

            var compras = await context.EntradaProductos
                .Include(x => x.IdProveedorNavigation)
                .Include(x => x.IdAlmacenNavigation)
                .Where(v => v.FechaRegistro >= dateStart && v.FechaRegistro <= dateEnd)
                .Select(x => new ComprasListadoDTO()
                {
                    Id = x.Id,
                    Proveedor = x.IdProveedorNavigation.Nombre,
                    Almacen = x.IdAlmacenNavigation.Descripcion,
                    SubTotal = x.SubTotal,
                    Total = x.Total,
                    FechaRegistro = x.FechaRegistro

                }).ToListAsync();

            return compras;
        }

        public async Task<List<CompraDetalleDTO>> listadoDetalleCompra(int id)
        {

            var entidad = await context.DetalleEntradaProductos
                .Include(x => x.IdProductoNavigation)
                .Where(X => X.IdEntradaProducto == id)
                .Select(x => new CompraDetalleDTO()
                {
                    Id = x.Id,
                    Producto = x.IdProductoNavigation.Descripcion,
                    Cantidad = x.Cantidad,
                    Precio = x.Precio,
                    Total = x.Total

                }).ToListAsync();

            return entidad;
        }

    }
}
