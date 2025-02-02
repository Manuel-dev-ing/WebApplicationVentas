using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Claims;
using WebApplicationVentas.DTOs;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Servicios;
using ZXing;
using ZXing.Common;

namespace WebApplicationVentas.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ApiProductosController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public ApiProductosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GenerarCodigoBarras")]
        public IActionResult GenerarCodigoBarras(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("El código no puede estar vacío.");
            }

            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 100,
                    Margin = 10
                }
            };

            var pixelData = writer.Write(code);
            using var bitmap = new Bitmap(pixelData.Width, pixelData.Height, PixelFormat.Format32bppRgb);
            using (var ms = new MemoryStream())
            {
                var bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, pixelData.Width, pixelData.Height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppRgb);
                try
                {
                    System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                bitmap.Save(ms, ImageFormat.Png);
                return File(ms.ToArray(), "image/png");

            }








        }


       

        //API Seccion Ventas
        [HttpGet("obtenerClientes")]
        public async Task<List<ClienteDTO>> clientes()
        {

            var clientes = await unitOfWork.repositorioVentas.listadoClientes();

            return clientes;
        }

        [HttpGet("obtenerDocumentosVentas")]
        public async Task<List<documentosVentasDTO>> documentosVentas()
        {
            var docVentas = await unitOfWork.repositorioVentas.documentosVentas();

            return docVentas;
        }

        [HttpGet("obtenerProductos")]
        public async Task<List<ProductosDTO>> productos()
        {

            var productos = await unitOfWork.repositorioVentas.listadoProductos();

            return productos;
        }

        //obtener productos por fecha
        [HttpPost("obtenerVentasFecha")]
        public async Task<ActionResult> obtenerProductoFecha([FromBody] ConsultarVentaDTO consultarVentaDTO)
        {
            if (string.IsNullOrWhiteSpace(consultarVentaDTO.fechaFin) && string.IsNullOrWhiteSpace(consultarVentaDTO.fechaInicio))
            {
                return BadRequest("Las fechas no pueden estar vacías. Por favor, proporciona ambas fechas.");
            }

            var resultado = await unitOfWork.repositorioVentas.obtenerVentasPorFecha(consultarVentaDTO.fechaInicio, consultarVentaDTO.fechaFin);

            return Ok(resultado);

        } 

        [HttpPost("venta")]
        public async Task<IActionResult> venta([FromBody] VentaDTO ventaDTO)
        {
            try
            {
                if (ventaDTO == null || ventaDTO.productos == null || string.IsNullOrWhiteSpace(ventaDTO.NombreCliente))
                {
                    return BadRequest("Datos invalidos");
                }


                // Validar stock y el producto existe 
                foreach (var item in ventaDTO.productos)
                {
                    var stockProducto = await unitOfWork.repositorioStockProductos.obtenerStockProductoPorId(item.IdProducto);
                    if (stockProducto == null)
                    {
                        return BadRequest(new { mensaje = $"No existte el producto {item.DescripcionProducto}" });

                    }
                    else
                    {
                        if (item.Cantidad > stockProducto.StockActual)
                        {
                            return BadRequest(new { mensaje = $"No hay Stock para el producto {item.DescripcionProducto}" });
                        }
                    }

                 
                }
                //fin validar stock

                var id_usuario = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var venta = new Venta()
                {
                    IdTipoDocumentoVenta = Convert.ToInt32(ventaDTO.idDocVenta),
                    IdUsuario = id_usuario,
                    NombreCliente = ventaDTO.NombreCliente,
                    SubTotal = Convert.ToDecimal(ventaDTO.SubTotal),
                    Total = Convert.ToDecimal(ventaDTO.Total),
                    FechaRegistro = DateTime.Now,
                    DetalleVenta = ventaDTO.productos.Select(x => new DetalleVenta
                    {
                        IdProducto = x.IdProducto,
                        Cantidad = x.Cantidad,
                        Precio = x.Precio,
                        Total = x.Total
                    }).ToList()
                };

                unitOfWork.repositorioVentas.crear(venta);


                foreach (var item in ventaDTO.productos)
                {
                    var stockProducto = await unitOfWork.repositorioStockProductos.obtenerStockProductoPorId(item.IdProducto);

                    stockProducto.StockActual -= item.Cantidad;
                    unitOfWork.repositorioStockProductos.actualizar(stockProducto);

                }


                await unitOfWork.Complete();
                return Ok();

            }
            catch (Exception ex)
            {

                return StatusCode(500, new { mensaje = "Ocurrió un error interno", detalle = ex.Message });
            }

        }

        // ENTRADA DE PRODUCTOS
        [HttpGet("obtenerAlmacenes")]
        public async Task<List<AlmacenesDTO>> almacenes()
        {

            var almacenes = await unitOfWork.repositorioCompras.listadoAlmacenes();

            return almacenes;
        }

        [HttpGet("obtenerProveedores")]
        public async Task<List<ProveedoresDTO>> proveedores()
        {

            var proveedores = await unitOfWork.repositorioCompras.listadoProveedores();

            return proveedores;
        }

        [HttpPost("entradaProducto")]
        public async Task<IActionResult> EntradaProducto([FromBody] CompraDTO compraDTO)
        {
            try
            {
                if (compraDTO == null || compraDTO.productos == null)
                {
                    return BadRequest("Datos invalidos");
                }

                var entrada = new EntradaProducto()
                {
                    IdAlmacen = Convert.ToInt32(compraDTO.IdAlmacen),
                    IdProveedor = Convert.ToInt32(compraDTO.IdProveedor),
                    FechaRegistro = DateTime.UtcNow,
                    SubTotal = compraDTO.SubTotal,
                    Total = compraDTO.Total,
                    DetalleEntradaProductos = compraDTO.productos.Select(a => new DetalleEntradaProducto()
                    {
                        IdProducto = a.IdProducto,
                        Cantidad = a.Cantidad,
                        Precio = a.Precio,
                        Total = a.Total
                    }).ToList()

                };

                unitOfWork.repositorioCompras.guardar(entrada);


                //Insertar en Stock Productos
                foreach (var item in compraDTO.productos)
                {
                    var stockProducto = await unitOfWork.
                                        repositorioStockProductos.obtenerProductoAlmacen(item.IdProducto, Convert.ToInt32(compraDTO.IdAlmacen));

                    if (stockProducto != null)
                    {
                        // si el producto ya existe en el stock, actualizar el registro
                        stockProducto.StockActual += item.Cantidad;
                        stockProducto.Precio = item.Precio;
                        unitOfWork.repositorioStockProductos.actualizar(stockProducto);

                    }
                    else
                    {
                        // si no existe, crear un nuevo registro en StockProducto
                        var nuevoStock = new StockProducto()
                        {
                            IdProducto = item.IdProducto,
                            IdAlmacen = Convert.ToInt32(compraDTO.IdAlmacen),
                            StockActual = item.Cantidad,
                            Precio = item.Precio
                        };

                        await unitOfWork.repositorioStockProductos.guardar(nuevoStock);
                    }

                }

                await unitOfWork.Complete();
                return Ok();

            }
            catch (Exception ex)
            {

                return StatusCode(500, new { mensaje = "Ocurrió un error interno", detalle = ex.Message });
            }


        }

    }
}
