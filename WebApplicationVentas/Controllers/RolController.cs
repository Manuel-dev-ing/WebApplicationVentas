using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class RolController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public RolController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(PaginacionViewModel paginacion)
        {
            var rolActivos = await unitOfWork.repositorioRol.rolActivo(paginacion);
            var total = unitOfWork.repositorioRol.contarElementos();

            var modelo = new PaginacionRespuesta<RolViewModel>()
            {
                ElementosActivos = rolActivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/Rol"

            };

            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> ElementosInactivos(PaginacionViewModel paginacion)
        {

            var rolInactivos = await unitOfWork.repositorioRol.rolInactivo(paginacion);
            var total = unitOfWork.repositorioRol.contarElementosInactivos();

            var modelo = new PaginacionRespuesta<RolViewModel>()
            {
                ElementosInactivos = rolInactivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/Rol/ElementosInactivos"
            };


            return View(modelo);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(RolCreacionViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var rol = new Rol()
            {
                Descripcion = model.Descripcion,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioRol.Guardar(rol);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Rol");

        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var existeRol = await unitOfWork.repositorioRol.existeRol(id);

            if (!existeRol)
            {
                return RedirectToAction("Index", "Rol");
            }

            var rol = await unitOfWork.repositorioRol.obtenerPorId(id);

            var modelo = new RolCreacionViewModel()
            {
                Id = rol.Id,
                Descripcion = rol.Descripcion,
                EsActivo = rol.EsActivo
            };

            return View(modelo);

        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(RolCreacionViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var rol = new Rol()
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                EsActivo = model.EsActivo,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioRol.Actualizar(rol);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Rol");

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existeRol = await unitOfWork.repositorioRol.existeRol(id);

            if (!existeRol)
            {
                return RedirectToAction("Index", "Rol");
            }

            var rol = await unitOfWork.repositorioRol.obtenerPorId(id);

            rol.EsActivo = false;

            unitOfWork.repositorioRol.Actualizar(rol);

            await unitOfWork.Complete();

            return RedirectToAction("Index", "Rol");
        }

        [HttpGet]
        public async Task<IActionResult> Restaurar(int id)
        {
            var existeRol = await unitOfWork.repositorioRol.existeRol(id);

            if (!existeRol)
            {
                return RedirectToAction("Index", "Rol");
            }

            var rol = await unitOfWork.repositorioRol.obtenerPorId(id);

            rol.EsActivo = true;

            unitOfWork.repositorioRol.Actualizar(rol);

            await unitOfWork.Complete();

            return RedirectToAction("Index", "Rol");
        }

    }
}
