using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class AlmacenesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AlmacenesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index(PaginacionViewModel paginacion)
        {

            var modelo = await unitOfWork.repositorioAlmacenes.obtenerAlmacenesActivos(paginacion);
            var totalAlmacenes = unitOfWork.repositorioAlmacenes.contarElementos();

            var almacenes = new PaginacionRespuesta<AlmacenViewModel>()
            {
                ElementosActivos = modelo,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = totalAlmacenes,
                BaseURL = "/Almacenes"

            };


            return View(almacenes);
        }


        [HttpGet]
        public async Task<IActionResult> ElementosInactivos(PaginacionViewModel paginacion)
        {
            var almaceesInactivos = await unitOfWork.repositorioAlmacenes.obteneAlmacenesInactivos(paginacion);
            var totalAlmacenes = unitOfWork.repositorioAlmacenes.contarElementosInactivos();

            var almacenes = new PaginacionRespuesta<AlmacenViewModel>()
            {
                ElementosInactivos = almaceesInactivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = totalAlmacenes,
                BaseURL = "/Almacenes/ElementosInactivos"

            };


            return View(almacenes);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Guardar(AlmacenViewModel modelo)
        {

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var almacen = new Almacene
            {
                Descripcion = modelo.Nombre,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioAlmacenes.crear(almacen);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Almacenes");
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var modelo = await unitOfWork.repositorioAlmacenes.obtenerAlmacenPorId(id);

            if (modelo is null)
            {
                return RedirectToAction("Index", "Almacenes");
            }

            var almacenViewModel = new AlmacenViewModel
            {
                Id = modelo.Id,
                Nombre = modelo.Descripcion,
                Esactivo = modelo.EsActivo,
                FechaRegistro = modelo.FechaRegistro
            };

            return View(almacenViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(AlmacenViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            if (modelo is null)
            {
                return RedirectToAction("Index", "Almacenes");
            }

            var almacen = new Almacene
            {
                Id = modelo.Id,
                Descripcion = modelo.Nombre,
                EsActivo = modelo.Esactivo,
                FechaRegistro = DateTime.UtcNow
            };


            unitOfWork.repositorioAlmacenes.actualizar(almacen);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Almacenes");

        }

        public async Task<IActionResult> Eliminar(int id)
        {

            var modelo = await unitOfWork.repositorioAlmacenes.obtenerAlmacenPorId(id);

            if (modelo is null)
            {
                return RedirectToAction("Index", "Almacenes");
            }

            modelo.EsActivo = false; 

            unitOfWork.repositorioAlmacenes.eliminar(modelo);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Almacenes");
        }

        public async Task<IActionResult> Restaurar(int id)
        {

            var modelo = await unitOfWork.repositorioAlmacenes.obtenerAlmacenPorId(id);

            if (modelo is null)
            {
                return RedirectToAction("Index", "Almacenes");
            }


            modelo.EsActivo = true;
            unitOfWork.repositorioAlmacenes.actualizar(modelo);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Almacenes");
        }



    }
}
