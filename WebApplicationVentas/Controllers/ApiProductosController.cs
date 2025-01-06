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

        [HttpPost("venta")]
        public async Task<IActionResult> venta([FromBody] VentaDTO ventaDTO)
        {
            try
            {
                if (ventaDTO == null || ventaDTO.productos == null || string.IsNullOrWhiteSpace(ventaDTO.NombreCliente))
                {
                    return BadRequest("Datos invalidos");
                }

                var id_usuario = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var venta = new Venta()
                {
                    IdTipoDocumentoVenta = Convert.ToInt32(ventaDTO.idDocVenta),
                    IdUsuario = id_usuario,
                    SubTotal = Convert.ToDecimal(ventaDTO.SubTotal),
                    Total = Convert.ToDecimal(ventaDTO.Total),
                    FechaRegistro = DateTime.Now,
                    DetalleVentas = ventaDTO.productos.Select(x => new DetalleVenta
                    {
                        IdProducto = x.IdProducto,
                        DescripcionProducto = x.DescripcionProducto,
                        Cantidad = x.Cantidad,
                        Precio = x.Precio,
                        Total = x.Total

                    }).ToList()
                };

                unitOfWork.repositorioVentas.crear(venta);
                await unitOfWork.Complete();

                return Ok(new { Mensaje = "Venta realizada con éxito" });

            }
            catch (Exception ex)
            {

                return StatusCode(500, new { mensaje = "Ocurrió un error interno", detalle = ex.Message });
            }

        }

        //[HttpPost("numero")]
        //public IActionResult numero([FromBody] PruebaDTO pruebaDTO)
        //{
        //    try
        //    {
        //        if (pruebaDTO == null || pruebaDTO.numero <= 0)
        //        {
        //            return BadRequest(new { mensaje = "Número no válido" });
        //        }

        //        return Ok(new { mensaje = $"Venta realizada con éxito para el número {pruebaDTO.numero}" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { mensaje = "Ocurrió un error interno", detalle = ex.Message });
        //    }
        //}

    }
}
