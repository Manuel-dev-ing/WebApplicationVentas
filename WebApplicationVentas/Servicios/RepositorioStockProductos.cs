using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioStockProductos
    {
        void actualizar(StockProducto stockProducto);
        Task<EditarStockProductosViewModel> edicionStockProductos(int id);
        void eliminar(StockProducto stockProducto);
        Task guardar(StockProducto stockProducto);
        Task<StockProducto> obtenerProductoAlmacen(int idProducto, int idAlmacen);
        Task<StockProducto> obtenerStockPorId(int id);
        Task<StockProducto> obtenerStockProductoPorId(int idProducto);
        Task<IEnumerable<StockProductosViewModel>> obtenerStockProductos();
    }


    public class RepositorioStockProductos: IRepositorioStockProductos
    {
        private readonly ApplicationDbContext context;

        public RepositorioStockProductos(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<EditarStockProductosViewModel> edicionStockProductos(int id)
        {
            var entidad = await context.StockProductos
                .Include(x => x.IdProductoNavigation)
                    .ThenInclude(a => a.IdCategoriaNavigation)
                .Include(x => x.IdProductoNavigation)
                    .ThenInclude(a => a.IdCategoriaNavigation)
                .Include(x => x.IdAlmacenNavigation)
                .Where(x => x.Id == id)
                .Select(x => new EditarStockProductosViewModel()
                {
                    Id = x.Id,
                    Stock = x.StockActual,
                    Almacen = x.IdAlmacenNavigation.Descripcion,
                    IdAlmacen = x.IdAlmacen,
                    IdProducto = x.IdProducto,
                    Imagen = x.IdProductoNavigation.Imagen,
                    Marca = x.IdProductoNavigation.IdMarcaNavigation.Descripcion,
                    Categoria = x.IdProductoNavigation.IdCategoriaNavigation.Descripcion,
                    Descripcion = x.IdProductoNavigation.Descripcion,
                    Precio = x.IdProductoNavigation.Precio

                }).FirstOrDefaultAsync();

            return entidad;
        }

        public async Task<IEnumerable<StockProductosViewModel>> obtenerStockProductos()
        {
            var entidad = await context.StockProductos
                .Include(a => a.IdProductoNavigation)
                .Include(a => a.IdAlmacenNavigation)
                .Select(x => new StockProductosViewModel()
                {
                    Id = x.Id,
                    Producto = x.IdProductoNavigation.Descripcion,
                    Almacen = x.IdAlmacenNavigation.Descripcion,
                    StockActual = x.StockActual,
                    Precio = x.Precio

                }).ToListAsync();

            return entidad;
        }

        public async Task guardar(StockProducto stockProducto)
        {
            await context.StockProductos.AddAsync(stockProducto);

        }

        public async Task<StockProducto> obtenerProductoAlmacen(int idProducto, int idAlmacen)
        {
            var entidad = await context.Set<StockProducto>().FirstOrDefaultAsync(x => x.IdProducto == idProducto && x.IdAlmacen == idAlmacen);
            
            return entidad;
        }

        public async Task<StockProducto> obtenerStockProductoPorId(int idProducto)
        {
            var entidad = await context.Set<StockProducto>().FirstOrDefaultAsync(x => x.IdProducto == idProducto);

            return entidad;
        }

        public async Task<StockProducto> obtenerStockPorId(int id)
        {
            var entidad = await context.Set<StockProducto>().FirstOrDefaultAsync(x => x.Id == id);

            return entidad;
        }

        public void actualizar(StockProducto stockProducto)
        {
            context.StockProductos.Update(stockProducto);
        }

        public void eliminar(StockProducto stockProducto)
        {
            context.StockProductos.Remove(stockProducto);
        }




    }
}
