using Microsoft.AspNetCore.Mvc;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class MarcasController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MarcasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var marcasActivas = await unitOfWork.repositorioMarcas.marcasActivas();
            var marcasInactivas = await unitOfWork.repositorioMarcas.marcasInactivas();

            var modelo = new MarcasModel()
            {
                marcasActivas = marcasActivas,
                marcasInactivas = marcasInactivas

            };

            return View(modelo);
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Guardar(MarcasViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Marcas");
            }

            var marca = new Marca()
            {
                Descripcion = model.Descripcion,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioMarcas.guardar(marca);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Marcas");
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var existeMarca = await unitOfWork.repositorioMarcas.existeMarca(id);

            if (!existeMarca)
            {
                return RedirectToAction("Index", "Marcas");
            }

            var modelo = await unitOfWork.repositorioMarcas.obtenerPorId(id);

            var marcaViewModel = new MarcasViewModel()
            {
                Id = modelo.Id,
                Descripcion = modelo.Descripcion,
                EsActivo = modelo.EsActivo,
                FechaCreacion = modelo.FechaRegistro
            };

            return View(marcaViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(MarcasViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var marca = await unitOfWork.repositorioMarcas.existeMarca(model.Id);

            if (!marca)
            {
                return RedirectToAction("Index", "Marcas");
            }

            var modelo = new Marca()
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                EsActivo = model.EsActivo,
                FechaRegistro = DateTime.UtcNow

            };

            unitOfWork.repositorioMarcas.editar(modelo);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Marcas");
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var marca = await unitOfWork.repositorioMarcas.existeMarca(id);

            if (!marca)
            {
                return RedirectToAction("Index", "Marcas");
            }

            var modelo = await unitOfWork.repositorioMarcas.obtenerPorId(id);
            modelo.EsActivo = false;

            unitOfWork.repositorioMarcas.eliminar(modelo);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Marcas");
        }

        public async Task<IActionResult> Restaurar(int id)
        {
            var marca = await unitOfWork.repositorioMarcas.existeMarca(id);

            if (!marca)
            {
                return RedirectToAction("Index", "Marcas");
            }
            
            var modelo = await unitOfWork.repositorioMarcas.obtenerPorId(id);

            modelo.EsActivo = true;

            unitOfWork.repositorioMarcas.editar(modelo);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Marcas");
        }

    }
}
