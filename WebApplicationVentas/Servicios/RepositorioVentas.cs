﻿using System.Globalization;
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
        Task<VentaFacturaViewModel> generarVentaFactura(int id);
        Task<List<ClienteDTO>> listadoClientes();
        Task<List<ProductosDTO>> listadoProductos();
        Task<List<VentaDetalleDTO>> obtenerDetalleVenta(int id);
        decimal obtenerGananciasVentas(string fechaInicio, string fechaFin);
        decimal obtenerGananciasVentasDia(string fecha);
        int obtenerTotalVentas(string fechaInicio, string fechaFin);
        int obtenerVentasDia(string fecha);
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
                .Include(x => x.IdClienteNavigation)
                .Where(v => v.FechaRegistro > dateStart && v.FechaRegistro < dateEnd)
                .Select(a => new VentasListadoDTO()
                {
                    Id = a.Id,
                    Usuario = a.IdUsuarioNavigation.Nombre + " " + a.IdUsuarioNavigation.Apellidos,
                    Cliente = a.IdClienteNavigation.Nombre + " " + a.IdClienteNavigation.Apellidos,
                    SubTotal = a.SubTotal,
                    Total = a.Total,
                    Fecha = a.FechaRegistro
                }).ToListAsync();

            return ventas;
        }

        public int obtenerTotalVentas(string fechaInicio, string fechaFin)
        {

            var dateStart = Convert.ToDateTime(fechaInicio);
            var dateEnd = Convert.ToDateTime(fechaFin);


            var ventas = context.Ventas
                .Where(v => v.FechaRegistro > dateStart && v.FechaRegistro < dateEnd)
                .Count();

            return ventas;
        }

        public int obtenerVentasDia(string fecha)
        {

            var dateStart = Convert.ToDateTime(fecha);

            var ventas = context.Ventas
                .Where(v => v.FechaRegistro.Date == dateStart)
                .Count();

            return ventas;
        }

        public decimal obtenerGananciasVentas(string fechaInicio, string fechaFin)
        {

            var dateStart = Convert.ToDateTime(fechaInicio);
            var dateEnd = Convert.ToDateTime(fechaFin);


            var ventas = context.Ventas
                .Where(v => v.FechaRegistro > dateStart && v.FechaRegistro < dateEnd)
                .Sum(x => x.Total);

            return ventas;
        }

        public decimal obtenerGananciasVentasDia(string fecha)
        {

            var dateStart = Convert.ToDateTime(fecha);

            var ventas = context.Ventas
                .Where(v => v.FechaRegistro == dateStart)
                .Sum(x => x.Total);

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

        public async Task<VentaFacturaViewModel> generarVentaFactura(int id)
        {

            var modelo = await context.Ventas
                .Include(x => x.IdTipoDocumentoVentaNavigation)
                .Include(x => x.IdClienteNavigation)
                .Where(x => x.Id == id)
                .Select(v => new VentaFacturaViewModel()
                {
                    NumeroVenta = v.NumeroVenta,
                    DocumentoCliente = v.IdTipoDocumentoVentaNavigation.Descripcion,
                    IdTipoDocumento = v.IdTipoDocumentoVenta,
                    NombreCliente = v.IdClienteNavigation.Nombre + ' ' + v.IdClienteNavigation.Apellidos,
                    SubTotal = v.SubTotal,
                    Total = v.Total,
                    productos = v.DetalleVenta.Select(d => new DetalleVentaFactura()
                    {
                        IdProducto = d.IdProducto,
                        DescripcionProducto = d.IdProductoNavigation.Descripcion,
                        Cantidad = d.Cantidad,
                        Precio = d.Precio,
                        Total = d.Total
                    }).ToList()

                }).FirstOrDefaultAsync();

            return modelo;
        }

    }

}
