using Microsoft.AspNetCore.Mvc;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class TiposDocumentosProvClienteController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public TiposDocumentosProvClienteController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(PaginacionViewModel paginacion)
        {
            var tiposDocumentosActivos = await unitOfWork.repositorioTiposDocumentosProv.registrosActivos(paginacion);
            var total = unitOfWork.repositorioTiposDocumentosProv.contarElementos();

            var modelo = new PaginacionRespuesta<TiposDocumentoViewModel>()
            {
                ElementosActivos = tiposDocumentosActivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/TiposDocumentosProvCliente"


            };


            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> ElementosInactivos(PaginacionViewModel paginacion)
        {
            var tiposDocumentosInactivos = await unitOfWork.repositorioTiposDocumentosProv.registrosInactivos(paginacion);

            var total = unitOfWork.repositorioTiposDocumentosProv.contarElementosInactivos();

            var almacenes = new PaginacionRespuesta<TiposDocumentoViewModel>()
            {
                ElementosInactivos = tiposDocumentosInactivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/TiposDocumentosProvCliente/ElementosInactivos"

            };


            return View(almacenes);
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(CreacionTiposDocumentosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "TiposDocumentosProvCliente");
            }

            var tiposDocumentos = new TiposDocumentosProvCliente()
            {
                Descripcion = model.Descripcion,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioTiposDocumentosProv.guardar(tiposDocumentos);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "TiposDocumentosProvCliente");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var existeRegistro = await unitOfWork.repositorioTiposDocumentosProv.existeRegistro(id);

            if (!existeRegistro)
            {
                return RedirectToAction("Index", "TiposDocumentosProvCliente");
            }

            var tiposDocumentos = await unitOfWork.repositorioTiposDocumentosProv.obtenerTiposDocumentos(id);

            var modelo = new CreacionTiposDocumentosViewModel()
            {
                Id = tiposDocumentos.Id,
                Descripcion = tiposDocumentos.Descripcion,
                Es_Activo = tiposDocumentos.EsActivo,
                Fecha = tiposDocumentos.FechaRegistro
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(CreacionTiposDocumentosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existeRegistro = await unitOfWork.repositorioTiposDocumentosProv.existeRegistro(model.Id);

            if (!existeRegistro)
            {
                return RedirectToAction("Index", "TiposDocumentosProvCliente");
            }

            var tipoDocumento = new TiposDocumentosProvCliente()
            {
                Id = model.Id,
                Descripcion = model.Descripcion,
                EsActivo = model.Es_Activo,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioTiposDocumentosProv.actualizar(tipoDocumento);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "TiposDocumentosProvCliente");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var entidad = await unitOfWork.repositorioTiposDocumentosProv.existeRegistro(id);

            if (!entidad)
            {
                return RedirectToAction("Index", "TiposDocumentosProvCliente");
            }

            var tiposDocumentos = await unitOfWork.repositorioTiposDocumentosProv.obtenerTiposDocumentos(id);
            tiposDocumentos.EsActivo = false;

            unitOfWork.repositorioTiposDocumentosProv.actualizar(tiposDocumentos);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "TiposDocumentosProvCliente");

        }

        public async Task<IActionResult> Restaurar(int id)
        {
            var entidad = await unitOfWork.repositorioTiposDocumentosProv.existeRegistro(id);

            if (!entidad)
            {
                return RedirectToAction("Index", "TiposDocumentosProvCliente");
            }

            var tiposDocumentos = await unitOfWork.repositorioTiposDocumentosProv.obtenerTiposDocumentos(id);
            tiposDocumentos.EsActivo = true;

            unitOfWork.repositorioTiposDocumentosProv.actualizar(tiposDocumentos);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "TiposDocumentosProvCliente");

        }


    }
}
