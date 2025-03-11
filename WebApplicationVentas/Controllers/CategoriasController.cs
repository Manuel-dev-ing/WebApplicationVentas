using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Reflection.Metadata.Ecma335;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoriasController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(PaginacionViewModel paginacion)
        {
            var categoriasActivas = await unitOfWork.repositorioCategorias.obtenerCategoriasActivas(paginacion);
            var totalCategorias = unitOfWork.repositorioCategorias.contarElementos();

            var categorias = new PaginacionRespuesta<CategoriaViewModel>()
            {
                ElementosActivos = categoriasActivas,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = totalCategorias,
                BaseURL = "/Categorias"

            };

            return View(categorias);
        }


        [HttpGet]
        public async Task<IActionResult> ElementosInactivos(PaginacionViewModel paginacion)
        {
            var categoriasInactivas = await unitOfWork.repositorioCategorias.obtenerCategoriasInactivas(paginacion);

            var totalAlmacenes = unitOfWork.repositorioCategorias.contarElementosInactivos();

            var almacenes = new PaginacionRespuesta<CategoriaViewModel>()
            {
                ElementosInactivos = categoriasInactivas,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = totalAlmacenes,
                BaseURL = "/Categorias/ElementosInactivos"

            };


            return View(almacenes);
        }


        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(CategoriaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var categoria = new Categoria()
            {
                Descripcion = model.Nombre,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioCategorias.guardarCategoria(categoria);
            await unitOfWork.Complete();


            return RedirectToAction("Index", "Categorias");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var entidad = await unitOfWork.repositorioCategorias.obtenerCategoriaPorId(id);

            if (entidad == null)
            {
                return RedirectToAction("Index", "Categorias");
            }

            var modelo = new CategoriaViewModel()
            {
                Id = entidad.Id,
                Nombre = entidad.Descripcion,
                Esactivo = entidad.EsActivo,
                FechaRegistro = entidad.FechaRegistro
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(CategoriaViewModel modelo)
        {

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var existeEntidad = await unitOfWork.repositorioCategorias.existeCategoriaPorId(modelo.Id);
            
            if (!existeEntidad)
            {
                return RedirectToAction("Index", "Categorias");

            }

            var entidad = new Categoria
            {
                Id = modelo.Id,
                Descripcion = modelo.Nombre,
                EsActivo = modelo.Esactivo,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioCategorias.editarCategoria(entidad);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Categorias");
        }

    
        public async Task<IActionResult> Eliminar(int id)
        {

            var existeCategoria = await unitOfWork.repositorioCategorias.existeCategoriaPorId(id);

            if (!existeCategoria)
            {
                return RedirectToAction("Index", "Categorias");
            }

            var modelo = await unitOfWork.repositorioCategorias.obtenerCategoriaPorId(id);

            modelo.EsActivo = false;
            unitOfWork.repositorioCategorias.EliminarCategoria(modelo);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Categorias");
        }

        public async Task<IActionResult> Restaurar(int id)
        {

            var modelo = await unitOfWork.repositorioCategorias.obtenerCategoriaPorId(id);

            if (modelo is null)
            {
                return RedirectToAction("Index", "Categorias");
            }


            modelo.EsActivo = true;
            unitOfWork.repositorioCategorias.editarCategoria(modelo);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Categorias");
        }



    }
}
