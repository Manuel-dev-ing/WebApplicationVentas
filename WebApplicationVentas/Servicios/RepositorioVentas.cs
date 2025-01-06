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

    }




    
}
