using Azure.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Rotativa.AspNetCore;
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
        private readonly IWebHostEnvironment webHost;

        public ApiProductosController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            this.unitOfWork = unitOfWork;
            this.webHost = webHost;
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

        //obtener ventas por fecha
        [HttpPost("obtenerVentasFecha")]
        public async Task<ActionResult> obtenerProductoFecha([FromBody] ConsultarVentaDTO consultarVentaDTO)
        {
            if (string.IsNullOrWhiteSpace(consultarVentaDTO.fechaFin) && string.IsNullOrWhiteSpace(consultarVentaDTO.fechaInicio))
            {
                return BadRequest("Las fechas no pueden estar vacías. Por favor, proporciona ambas fechas.");
            }

            var resultado = await unitOfWork.repositorioVentas.obtenerVentasPorFecha(consultarVentaDTO.fechaInicio, consultarVentaDTO.fechaFin);

            if (resultado.Count() == 0)
            {
                return BadRequest(new { mensaje = "No se encontraron resultados" });
            }

            return Ok(resultado);

        } 

        [HttpPost("venta")]
        public async Task<IActionResult> venta([FromBody] VentaDTO ventaDTO)
        {

            try
            {
                var tipo_documento = "";

                if (ventaDTO == null || ventaDTO.productos == null || !ventaDTO.productos.Any())
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

                var id_doc_venta = Convert.ToUInt32(ventaDTO.idDocVenta);
                if (id_doc_venta == 2)
                {
                    tipo_documento = "factura";
                }
                if (id_doc_venta == 1)
                {
                    tipo_documento = "ticket";
                }


                var venta = new Venta()
                {
                    IdTipoDocumentoVenta = Convert.ToInt32(ventaDTO.idDocVenta),
                    IdUsuario = id_usuario == 0 ? 1 : id_usuario,
                    IdCliente = Convert.ToInt32(ventaDTO.idCliente),
                    NumeroVenta = GenerarNumeroVenta(),
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
                return Ok(new { id_venta = venta.Id, documento = tipo_documento });

            }
            catch (Exception ex)
            {

                return StatusCode(500, new { mensaje = "Ocurrió un error interno", detalle = ex.Message });
            }

        }

        private int GenerarNumeroVenta()
        {
            Random random = new Random();
            return random.Next(100000, 999999); // Número de 6 dígitos
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<DetalleVentaDTO>>> Get(int id)
        {

            var productos = await unitOfWork.repositorioVentas.obtenerDetalleVenta(id);

            if (productos is null)
            {
                return NotFound();
            }


            return Ok(productos);
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

        //obtener compras por fecha
        [HttpPost("obtenerComprasFecha")]
        public async Task<ActionResult> obtenerComprasFecha([FromBody] ConsultarComprasDTO consultarComprasDTO)
        {
            if (string.IsNullOrWhiteSpace(consultarComprasDTO.fechaFin) && string.IsNullOrWhiteSpace(consultarComprasDTO.fechaInicio))
            {
                return BadRequest("Las fechas no pueden estar vacías. Por favor, proporciona ambas fechas.");
            }

            var resultado = await unitOfWork.repositorioCompras.obtenerComprasPorFecha(consultarComprasDTO.fechaInicio, consultarComprasDTO.fechaFin);

            if (resultado.Count() == 0)
            {
                return BadRequest(new { mensaje = "No se encontraron resultados" });
            }

            return Ok(resultado);

        }
        
        
        [HttpGet("detalleCompra/{id:int}")]
        public async Task<ActionResult<List<DetalleVentaDTO>>> detalleCompra(int id)
        {

            var productos = await unitOfWork.repositorioCompras.listadoDetalleCompra(id);

            if (productos is null)
            {
                return NotFound();
            }


            return Ok(productos);
        }

        [HttpGet("tieneStock/{id:int}")]
        public async Task<ActionResult> tieneStock(int id)
        {
            //obtener el stock por el id del producto
            var entidad = await unitOfWork.repositorioStockProductos.obtenerStockProductoPorId(id);
            if (entidad == null)
            {
                var producto = await unitOfWork.repositorioProductos.obtenerProductoPorId(id);
                producto.EsActivo = false;

                unitOfWork.repositorioProductos.Eliminar(producto);
                eliminarImagen(producto);

                await unitOfWork.Complete();

                return Ok(new { mensaje = "Producto eliminado correctamente", tipo = "exito" });
            }

            return Ok(new { mensaje = "El Producto contiene stock disponible", tipo = "error" });
        }


        [HttpPost("eliminarProducto")]
        public async Task<ActionResult> eliminarProducto([FromBody] int id)
        {

            var existeProducto = await unitOfWork.repositorioProductos.existeProducto(id);
            if (!existeProducto)
            {
                return BadRequest();
            }

            var stockProducto = await unitOfWork.repositorioStockProductos.obtenerStockProductoPorId(id);

            var producto = await unitOfWork.repositorioProductos.obtenerProductoPorId(id);
            producto.EsActivo = false;

            unitOfWork.repositorioStockProductos.eliminar(stockProducto);
            unitOfWork.repositorioProductos.Eliminar(producto);

            await unitOfWork.Complete();
            

            return Ok();
        }



        //[HttpGet("ticket/{id:int}")]
        //public async Task<IActionResult> ticket(int id)
        //{

        //    var producto = await unitOfWork.repositorioVentas.generarVentaFactura(id);

        //    if (producto is null)
        //    {
        //        return NotFound();
        //    }

        //    var pdf = new ViewAsPdf("ImprimirTicket", producto)
        //    {
        //        FileName = $"Ticket {producto.NumeroVenta}.pdf",
        //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
        //        PageSize = Rotativa.AspNetCore.Options.Size.B7
        //    };

        //    //var pdfBytes = await pdf.BuildFile(ControllerContext);

        //    return Ok();
        //}


        //Obtiene los productos con el stock para la notificacion
        [HttpGet("obtenerProductosStock")]
        public async Task<ActionResult> obtenerProductosStock()
        {
            var resultado = await unitOfWork.repositorioProductos.ListadoProductosStock();

            if (resultado is null)
            {
                return NotFound();
            }

            foreach (var item in resultado)
            {

                item.ListadoProductos = await unitOfWork.repositorioProductos.ProductosListado(item.IdProducto); 

            }

            return Ok(resultado);
        }

        //Obtener datos del Negocio
        [HttpGet]
        public async Task<ActionResult> obtenerDatosNegocio()
        {
            var modelo = await unitOfWork.repositorioNegocio.obtener();

            if (modelo is null)
            {
                return NotFound();
            }

            return Ok(modelo);
        }

        private void eliminarImagen(Producto producto)
        {
            if (producto != null)
            {
                string carpeta = Path.Combine(webHost.WebRootPath, "Imagenes");
                string imagen = Path.Combine(Directory.GetCurrentDirectory(), carpeta, producto.Imagen);

                if (imagen != null)
                {
                    if (System.IO.File.Exists(imagen))
                    {
                        System.IO.File.Delete(imagen);
                    }
                }
            }
        }

    }
}
