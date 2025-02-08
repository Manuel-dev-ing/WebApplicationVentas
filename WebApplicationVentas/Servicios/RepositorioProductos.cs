using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.DTOs;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioProductos
    {
        void Actualizar(Producto producto);
        void Eliminar(Producto producto);
        Task<bool> existeProducto(int id);
        void Guardar(Producto producto);
        Task<List<ListadoProductosStockDTO>> ListadoProductosStock();
        Task<Producto> obtenerProductoPorId(int id);
        Task<IEnumerable<ProductosListadoViewModel>> productosActivos();
        Task<IEnumerable<ProductosListadoViewModel>> productosInactivos();
        Task<List<ProductosListadoDTO>> ProductosListado(int id);
    }


    public class RepositorioProductos: IRepositorioProductos
    {
        private readonly ApplicationDbContext context;

        public RepositorioProductos(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductosListadoViewModel>> productosActivos()
        {
            var producto = await context.Productos
                .Include(x => x.IdMarcaNavigation)
                .Include(x => x.IdCategoriaNavigation)
                .Where(x => x.EsActivo == true).Select(a => new ProductosListadoViewModel()
            {
                Id = a.Id,
                Marca = a.IdMarcaNavigation.Descripcion,
                Categoria = a.IdCategoriaNavigation.Descripcion,
                Descripcion = a.Descripcion,
                Precio = a.Precio,
                Fecha = a.FechaRegistro
            }).ToListAsync();


            return producto;
        }

        public async Task<IEnumerable<ProductosListadoViewModel>> productosInactivos()
        {
            var producto = await context.Productos
               .Include(x => x.IdMarcaNavigation)
               .Include(x => x.IdCategoriaNavigation)
               .Where(x => x.EsActivo == false).Select(a => new ProductosListadoViewModel()
               {
                   Id = a.Id,
                   Marca = a.IdMarcaNavigation.Descripcion,
                   Categoria = a.IdCategoriaNavigation.Descripcion,
                   Descripcion = a.Descripcion,
                   Precio = a.Precio,
                   Fecha = a.FechaRegistro
               }).ToListAsync();


            return producto;
        }

        public void Guardar(Producto producto)
        {
            context.Productos.Add(producto);
        }

        public void Eliminar(Producto producto)
        {

            context.Productos.Update(producto);
        
        }

        public void Actualizar(Producto producto)
        {

            context.Productos.Update(producto);

        }

        public async Task<bool> existeProducto(int id)
        {
            var producto = await context.Productos.AnyAsync(x => x.Id == id);
            return producto;
        }

        public async Task<Producto> obtenerProductoPorId(int id)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(x => x.Id == id);
            return producto;

        }

        public async Task<List<ProductosListadoDTO>> ProductosListado(int id)
        {
            var producto = await context.Productos
           .Include(x => x.IdMarcaNavigation)
           .Include(x => x.IdCategoriaNavigation)
           .Where(x => x.EsActivo == true && x.Id == id).Select(a => new ProductosListadoDTO()
           {
               Id = a.Id,
               Marca = a.IdMarcaNavigation.Descripcion,
               Categoria = a.IdCategoriaNavigation.Descripcion,
               Descripcion = a.Descripcion,
               StockMinimo = a.StockMinimo,
               StockMaximo = a.StockMaximo

           }).ToListAsync();

            return producto;

        }

        public async Task<List<ListadoProductosStockDTO>> ListadoProductosStock()
        {
            var entidad = await context.StockProductos.Select(x => new ListadoProductosStockDTO()
            {
                Id = x.Id,
                IdProducto = x.IdProducto,
                StockActual = x.StockActual,
                Precio = x.Precio

            }).ToListAsync();
            
            
            return entidad;
        }



    }
}
