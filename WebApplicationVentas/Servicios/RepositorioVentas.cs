using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.DTOs;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioVentas
    {
        void crear(Venta venta);
        Task<List<documentosVentasDTO>> documentosVentas();
        Task<List<ClienteDTO>> listadoClientes();
        Task<List<ProductosDTO>> listadoProductos();
        Task<List<VentaDetalleDTO>> obtenerDetalleVenta(int id);
        Task<List<VentasListadoDTO>> obtenerVentasPorFecha(string fechaInicio, string fechaFin);
    }



    public class RepositorioVentas: IRepositorioVentas
    {
        private readonly ApplicationDbContext context;

        public RepositorioVentas(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<ClienteDTO>> listadoClientes()
        {

            var entidad = await context.Clientes.Where(x => x.EsActivo == true).Select(a => new ClienteDTO()
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellidos = a.Apellidos,
                Email = a.Email,
                Telefono = a.Telefono,
                Calle = a.Calle,
                Colonia = a.Colonia,
                CodigoPostalCiudad = a.CodigoPostalCiudad

            }).ToListAsync();

            return entidad;
        }


        public async Task<List<documentosVentasDTO>> documentosVentas()
        {
            var entidad = await context.TipoDocumentoVenta.Where(x => x.EsActivo == true).Select(a => new documentosVentasDTO()
            {
                Id = a.Id,
                Descripcion = a.Descripcion,
                EsActivo = a.EsActivo,
                FechaRegistro = a.FechaRegistro

            }).ToListAsync();

            return entidad;
        }


        public async Task<List<ProductosDTO>> listadoProductos() {
            var producto = await context.Productos
            .Include(x => x.IdMarcaNavigation)
            .Include(x => x.IdCategoriaNavigation)
            .Where(x => x.EsActivo == true).Select(a => new ProductosDTO()
            {
                Id = a.Id,
                Marca = a.IdMarcaNavigation.Descripcion,
                Categoria = a.IdCategoriaNavigation.Descripcion,
                Descripcion = a.Descripcion,
                Precio = a.Precio,
                CodigoBarras = a.CodigoBarras,
                Fecha = a.FechaRegistro
            }).ToListAsync();


            return producto;

        }

        public void crear(Venta venta)
        {
            context.AddAsync(venta);
        }

        public async Task<List<VentasListadoDTO>> obtenerVentasPorFecha(string fechaInicio, string fechaFin)
        {
            var dateStart = Convert.ToDateTime(fechaInicio);
            var dateEnd = Convert.ToDateTime(fechaFin);


            var ventas = await context.Ventas
                .Include(x => x.IdUsuarioNavigation)
                .Where(v => v.FechaRegistro >= dateStart && v.FechaRegistro <= dateEnd)
                .Select(a => new VentasListadoDTO(){
                    Id = a.Id,
                    Usuario = a.IdUsuarioNavigation.Nombre + " " + a.IdUsuarioNavigation.Apellidos,
                    Cliente = a.NombreCliente,
                    SubTotal = a.SubTotal,
                    Total = a.Total,
                    Fecha = a.FechaRegistro
                }).ToListAsync();

            return ventas;
        }

        public async Task<List<VentaDetalleDTO>> obtenerDetalleVenta(int id)
        {
            var entidad = await context.DetalleVentas
                .Include(x => x.IdProductoNavigation)
                .Where(x => x.IdVenta == id)
                .Select(x => new VentaDetalleDTO()
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
