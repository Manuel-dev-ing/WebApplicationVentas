using Microsoft.AspNetCore.Mvc;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class RubrosController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public RubrosController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rubrosActivos = await unitOfWork.repositorioRubros.rubrosActvos();
            var rubrosInactivos = await unitOfWork.repositorioRubros.rubrosInactvos();

            var modelo = new RubrosModel()
            {
                rubrosActivos = rubrosActivos,
                rubrosInactivos = rubrosInactivos
            };

            return View(modelo);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Guardar(RubrosViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var rubro = new Rubro()
            {
                Descripcion = model.Descripcion,
                FechaRegistro = DateTime.UtcNow,
                EsActivo = true
            };

            unitOfWork.repositorioRubros.guardarRubro(rubro);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Rubros");
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var rubro = await unitOfWork.repositorioRubros.existeRubro(id);

            if (!rubro)
            {
                return RedirectToAction("Index", "Rubros");
            }

            var modelo = await unitOfWork.repositorioRubros.obtenerRubroPorId(id);

            var rubroViewModel = new RubrosViewModel()
            {
                Id = modelo.Id,
                Descripcion = modelo.Descripcion,
                EsActivo = modelo.EsActivo,
                FechaRegistro = modelo.FechaRegistro
            };


            return View(rubroViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Actualizar(RubrosViewModel rubrosViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(rubrosViewModel);
            }

            var existeRubro = await unitOfWork.repositorioRubros.existeRubro(rubrosViewModel.Id);
            if (!existeRubro)
            {
                return View(rubrosViewModel);
            }

            var rubro = new Rubro()
            {
                Id = rubrosViewModel.Id,
                Descripcion = rubrosViewModel.Descripcion,
                EsActivo = rubrosViewModel.EsActivo,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioRubros.editarRubro(rubro);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Rubros");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existeRubro = await unitOfWork.repositorioRubros.existeRubro(id);
            if (!existeRubro)
            {
                return RedirectToAction("Index", "Rubros");
            }

            var modelo = await unitOfWork.repositorioRubros.obtenerRubroPorId(id);

            modelo.EsActivo = false;

            unitOfWork.repositorioRubros.editarRubro(modelo);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Rubros");

        }
        public async Task<IActionResult> Restaurar(int id)
        {
            var existeRubro = await unitOfWork.repositorioRubros.existeRubro(id);

            if (!existeRubro)
            {
                return RedirectToAction("Index", "Rubros");
            }


            var modelo = await unitOfWork.repositorioRubros.obtenerRubroPorId(id);

            modelo.EsActivo = true;

            unitOfWork.repositorioRubros.editarRubro(modelo);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Rubros");
        }

    }
}
