using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ProveedoresController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(PaginacionViewModel paginacion)
        {
            var proveedoreActivos = await unitOfWork.repositorioProveedores.proveedoresActivos(paginacion);
            var totalProveedores = unitOfWork.repositorioProveedores.contarElementos();

            var modelo = new PaginacionRespuesta<ProvedoresViewModel>()
            {
                ElementosActivos = proveedoreActivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = totalProveedores,
                BaseURL = "/Proveedores"
            };

            return View(modelo);
        }
        
        public async Task<IActionResult> Crear()
        {
            var modelo = new ProveedoresCreacionViewModel();
            modelo.tiposRubros = await obtenerTiposRubros();
            modelo.tiposDocumentos = await obtenerTiposDocumentos();


            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(ProveedoresCreacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Proveedores");
            }

            var proveedor = new Proveedore()
            {
                IdRubro = model.IdRubro,
                IdTipoDocumento = model.IdTipoDocuemto,
                Nombre = model.Nombre,
                Apellidos = model.Apellidos,
                Email = model.Email,
                Telefono = model.Telefono,
                Calle = model.Calle,
                Colonia = model.Colonia,
                CodigoPostalCiudad = model.CodigoPostalCiudad,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioProveedores.guardar(proveedor);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Proveedores");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var proveedor = await unitOfWork.repositorioProveedores.existeProveedor(id);
            if (!proveedor)
            {
                return RedirectToAction("Index", "Proveedores");
            }

            var proveedore = await unitOfWork.repositorioProveedores.obtenerProveedorPorId(id);

            var modelo = new ProveedoresCreacionViewModel()
            {
                Id = proveedore.Id,
                IdTipoDocuemto = proveedore.IdTipoDocumento,
                IdRubro = proveedore.IdRubro,
                Nombre = proveedore.Nombre,
                Apellidos = proveedore.Apellidos,
                Email = proveedore.Email,
                Telefono = proveedore.Telefono,
                Calle = proveedore.Calle,
                Colonia = proveedore.Colonia,
                CodigoPostalCiudad = proveedore.CodigoPostalCiudad,
                EsActivo = proveedore.EsActivo,
                tiposDocumentos = await obtenerTiposDocumentos(),
                tiposRubros = await obtenerTiposRubros()

            };

            return View(modelo);

        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(ProveedoresCreacionViewModel model)
        {

            if (!ModelState.IsValid) 
            {
                return RedirectToAction("Index", "Proveedores");
            }

            var proveedor = new Proveedore()
            {
                Id = model.Id,
                IdTipoDocumento = model.IdTipoDocuemto,
                IdRubro = model.IdRubro,
                Nombre = model.Nombre,
                Apellidos = model.Apellidos,
                Email = model.Email,
                Telefono = model.Telefono,
                Calle = model.Calle,
                Colonia = model.Colonia,
                CodigoPostalCiudad = model.CodigoPostalCiudad,
                EsActivo = model.EsActivo,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioProveedores.actualizar(proveedor);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Proveedores");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {

            var existeProveedor = await unitOfWork.repositorioProveedores.existeProveedor(id);

            if (!existeProveedor)
            {
                return RedirectToAction("Index", "Proveedores");
            }

            var proveedor = await unitOfWork.repositorioProveedores.obtenerProveedorPorId(id);
            proveedor.EsActivo = false;

            unitOfWork.repositorioProveedores.actualizar(proveedor);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Proveedores");

        }

        [HttpGet]
        public async Task<IActionResult> Restaurar(int id)
        {

            var existeProveedor = await unitOfWork.repositorioProveedores.existeProveedor(id);

            if (!existeProveedor)
            {
                return RedirectToAction("Index", "Proveedores");
            }

            var proveedor = await unitOfWork.repositorioProveedores.obtenerProveedorPorId(id);
            proveedor.EsActivo = true;

            unitOfWork.repositorioProveedores.actualizar(proveedor);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Proveedores");

        }

        [HttpGet]
        public async Task<IActionResult> ElementosInactivos(PaginacionViewModel paginacion)
        {
            var proveedoresInactivos = await unitOfWork.repositorioProveedores.proveedoresInactivos(paginacion);

            var total = unitOfWork.repositorioProveedores.contarElementosInactivos();

            var modelo = new PaginacionRespuesta<ProvedoresViewModel>()
            {

                ElementosInactivos = proveedoresInactivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/Proveedores/ElementosInactivos"

            };


            return View(modelo);
        }

        private async Task<IEnumerable<SelectListItem>> obtenerTiposDocumentos()
        {
            var tiposDocumentos = await unitOfWork.repositorioTiposDocumentosProv.ListadoregistrosActivos();
            var resultado = tiposDocumentos.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione un Rubro --", "0", true);
            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }
        private async Task<IEnumerable<SelectListItem>> obtenerTiposRubros()
        {
            var tiposRubros = await unitOfWork.repositorioRubros.listadoActvos();
            var resultado = tiposRubros.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione un Rubro --", "0", true);
            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }

    }
}
