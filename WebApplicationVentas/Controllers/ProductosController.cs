using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        public async Task<IActionResult> Index()
        {
            var registrosActivos = await unitOfWork.repositorioProductos.productosActivos();
            var registrosInactivos = await unitOfWork.repositorioProductos.productosInactivos();

            var modelo = new ProductosModel()
            {
                ProductosActivos = registrosActivos,
                ProductosInactivos = registrosInactivos
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

            //if (model.StockMinimo > model.StockMaximo)
            //{
            //    TempData["Mensaje"] = "El stock minimo no puede ser mayor que el stock maximo";
            //    return View(model);

            //}
            //else if (model.StockMaximo < model.StockMinimo)
            //{
            //    TempData["Mensaje"] = "El stock maximo no puede ser menor que el stock minimo";
            //    return RedirectToAction("Crear", "Productos");

            //}
            //else if(model.StockMaximo == model.StockMinimo)
            //{
            //    TempData["Mensaje"] = "La Cantidad de stocks no puede ser iguales";
            //    return RedirectToAction("Crear", "Productos");
            //}


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
        public async Task<IActionResult> Actualizar(ProductosCreacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
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
                return RedirectToAction("Index", "Productos");
            }

            var producto = await unitOfWork.repositorioProductos.obtenerProductoPorId(id);
            producto.EsActivo = true;

            unitOfWork.repositorioProductos.Actualizar(producto);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Productos");
        }


        private async Task<IEnumerable<SelectListItem>> obtenerTiposMarcas()
        {
            var tiposMarcas = await unitOfWork.repositorioMarcas.marcasActivas();
            var resultado = tiposMarcas.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione una Marca --", "0", true);
            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }

        private async Task<IEnumerable<SelectListItem>> obtenerTiposCategorias()
        {
            var tiposCategorias = await unitOfWork.repositorioCategorias.obtenerCategoriasActivas();
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
