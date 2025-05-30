﻿using System.Globalization;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Rotativa.AspNetCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHost;

        public ProductosController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            this.unitOfWork = unitOfWork;
            this.webHost = webHost;
        }


        [HttpGet]
        public async Task<IActionResult> Index(PaginacionViewModel paginacion)
        {
            var registrosActivos = await unitOfWork.repositorioProductos.productosActivos(paginacion);
            var totalProductos = unitOfWork.repositorioProductos.contarElementos();

            var modelo = new PaginacionRespuesta<ProductosListadoViewModel>()
            {
                ElementosActivos = registrosActivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = totalProductos,
                BaseURL = "/Productos"
            };

            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var productoModel = new ProductosCreacionViewModel();

            productoModel.tiposCategorias = await obtenerTiposCategorias();
            productoModel.tiposMarcas = await obtenerTiposMarcas();


            return View(productoModel);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductosCreacionViewModel model)
        {
            if (!ModelState.IsValid) {
                model.tiposMarcas = await obtenerTiposMarcas();
                model.tiposCategorias = await obtenerTiposCategorias();
                return View(model);
            }

            string nombreArchivo = subirImagen(model);

            var producto = new Producto()
            {
                IdMarca = model.IdMarca,
                IdCategoria = model.IdCategoria,
                CodigoBarras = model.CodigoBarras,
                Descripcion = model.Descripcion,
                StockMinimo = model.StockMinimo,
                StockMaximo = model.StockMaximo,
                Imagen = nombreArchivo,
                Precio = model.Precio,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioProductos.Guardar(producto);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Productos");

        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var existeProducto = await unitOfWork.repositorioProductos.existeProducto(id);
            if (!existeProducto)
            {
                return RedirectToAction("Index", "Productos");
            }

            var modelo = await unitOfWork.repositorioProductos.obtenerProductoPorId(id);

            var producto = new ProductosCreacionViewModel()
            {
                Id = modelo.Id,
                IdMarca = modelo.IdMarca,
                IdCategoria = modelo.IdCategoria,
                CodigoBarras = modelo.CodigoBarras,
                Descripcion = modelo.Descripcion,
                StockMinimo = modelo.StockMinimo,
                StockMaximo = modelo.StockMaximo,
                ImagenProducto = modelo.Imagen,
                Precio = modelo.Precio,
                EsActivo = modelo.EsActivo,
                FechaRegistro = modelo.FechaRegistro,
                tiposCategorias = await obtenerTiposCategorias(),
                tiposMarcas = await obtenerTiposMarcas()
            };

            return View(producto);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProductosCreacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.tiposCategorias = await obtenerTiposCategorias();
                model.tiposMarcas = await obtenerTiposMarcas();
                return View(model);    
            }

            string nombreArchivo = model.ImagenProducto;

            if (model.ImagenArchivo != null)
            {
                string rutaArchivo = Path.Combine(webHost.WebRootPath, "Imagenes", model.ImagenProducto);
                if (System.IO.File.Exists(rutaArchivo))
                {
                    System.IO.File.Delete(rutaArchivo);
                }

                nombreArchivo = subirImagen(model);
            }

            var producto = new Producto()
            {
                Id = model.Id,
                IdMarca = model.IdMarca,
                IdCategoria = model.IdCategoria,
                CodigoBarras = model.CodigoBarras,
                Descripcion = model.Descripcion,
                StockMinimo = model.StockMinimo,
                StockMaximo = model.StockMaximo,
                Imagen = nombreArchivo,
                Precio = model.Precio,
                EsActivo = model.EsActivo,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioProductos.Actualizar(producto);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Productos");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var modelo = await unitOfWork.repositorioProductos.existeProducto(id);
            if (!modelo)
            {
                return RedirectToAction("Index", "Productos");
            }

            var producto = await unitOfWork.repositorioProductos.obtenerProductoPorId(id);
            producto.EsActivo = false;

            unitOfWork.repositorioProductos.Eliminar(producto);
            eliminarImagen(producto);

            await unitOfWork.Complete();
            return RedirectToAction("Index", "Productos");
        }

        [HttpGet]
        public async Task<IActionResult> Restaurar(int id)
        {
            var exiteModelo = await unitOfWork.repositorioProductos.existeProducto(id);
            
            if (!exiteModelo)
            {
                var producto = await unitOfWork.repositorioProductos.obtenerProductoPorId(id);
                producto.EsActivo = true;

                unitOfWork.repositorioProductos.Actualizar(producto);
                await unitOfWork.Complete();
                return RedirectToAction("Index", "Productos");
            }

         

            return RedirectToAction("Index", "Productos");
        }

        [HttpGet]
        public async Task<IActionResult> ElementosInactivos(PaginacionViewModel paginacion)
        {
            var productosInactivas = await unitOfWork.repositorioProductos.productosInactivos(paginacion);

            var total = unitOfWork.repositorioProductos.contarElementosInactivos();

            var almacenes = new PaginacionRespuesta<ProductosListadoViewModel>()
            {
                ElementosInactivos = productosInactivas,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/Productos/ElementosInactivos"

            };


            return View(almacenes);
        }


        [HttpGet]
        public async Task<IActionResult> ImprimirVenta(int id)
        {

            var modelo = await unitOfWork.repositorioVentas.generarVentaFactura(id);
            var negocioModel = await unitOfWork.repositorioNegocio.obtener();

            modelo.logotipo = negocioModel.ImagenLogotipo;
            modelo.Direccion = negocioModel.Calle + " " + negocioModel.Colonia;
            modelo.Correo = negocioModel.Correo;

            if (modelo is null)
            {
                return RedirectToAction("Index", "Productos");
            }

            return new ViewAsPdf("ImprimirVenta", modelo)
            {
                FileName = $"Venta {modelo.NumeroVenta}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4

            };
        }

        [HttpGet]
        public async Task<IActionResult> imprimirTicket(int id)
        {

            var producto = await unitOfWork.repositorioVentas.generarVentaFactura(id);
            var negocioModel = await unitOfWork.repositorioNegocio.obtener();
            producto.NombreNegocio = negocioModel.Nombre;
          
            if (producto is null)
            {
                return RedirectToAction("Index", "Productos");
            }

            return new ViewAsPdf("ImprimirTicket", producto)
            {
                FileName = $"Ticket-{producto.NumeroVenta}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.B7
            };

        }


        private async Task<IEnumerable<SelectListItem>> obtenerTiposMarcas()
        {
            var tiposMarcas = await unitOfWork.repositorioMarcas.ListadomarcasActivas();
            var resultado = tiposMarcas.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione una Marca --", "0", true);
            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }

        private async Task<IEnumerable<SelectListItem>> obtenerTiposCategorias()
        {
            var tiposCategorias = await unitOfWork.repositorioCategorias.CategoriasActivas();
            var resultado = tiposCategorias.Select(x => new SelectListItem(x.Nombre, x.Id.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione una Categoria --", "0", true);
            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }

        private string subirImagen(ProductosCreacionViewModel model)
        {
            string nombreArchivo = "";

            string upload = Path.Combine(webHost.WebRootPath, "Imagenes");
            nombreArchivo = Guid.NewGuid().ToString() + "_" + model.ImagenArchivo.FileName;
            string rutaArchivo = Path.Combine(upload, nombreArchivo);
            model.ImagenArchivo.CopyTo(new FileStream(rutaArchivo, FileMode.Create));

            return nombreArchivo;

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
