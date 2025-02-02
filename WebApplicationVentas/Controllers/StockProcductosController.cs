using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class StockProcductosController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public StockProcductosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entidad = await unitOfWork.repositorioStockProductos.obtenerStockProductos();

            return View(entidad);
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var modelo = await unitOfWork.repositorioStockProductos.edicionStockProductos(id);


            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(EditarStockProductosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var entidad = new StockProducto()
            {
                Id = model.Id,
                IdProducto = model.IdProducto,
                IdAlmacen = model.IdAlmacen,
                StockActual = model.Stock,
                Precio = model.Precio
            };

            unitOfWork.repositorioStockProductos.actualizar(entidad);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "StockProcductos");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = await unitOfWork.repositorioStockProductos.obtenerStockPorId(id);

            if (entidad == null)
            {
                return RedirectToAction("Index", "StockProcductos");

            }

            unitOfWork.repositorioStockProductos.eliminar(entidad);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "StockProcductos");
        }

    }
}
