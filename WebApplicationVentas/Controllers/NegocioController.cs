using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class NegocioController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHost;

        public NegocioController(IUnitOfWork unitOfWork, IWebHostEnvironment webHost)
        {
            this.unitOfWork = unitOfWork;
            this.webHost = webHost;
        }


        public async Task<IActionResult> Index()
        {
            var modelo = await unitOfWork.repositorioNegocio.obtener();

            if (modelo is null)
            {
                return RedirectToAction("Crear", "Negocio");
            }

            return View(modelo);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(NegocioCreacionViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            if (modelo is null)
            {
                return RedirectToAction("Crear", "Negocio");
            }

            string nombreArchivo = subirImagen(modelo);

            var entidad = new Negocio()
            {
                
                Nombre = modelo.Nombre,
                Telefono = modelo.Telefono,
                Correo = modelo.Correo,
                Calle = modelo.Calle,
                Colonia = modelo.Colonia,
                Logotipo = nombreArchivo
            };



            unitOfWork.repositorioNegocio.guardar(entidad);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Negocio");
        }

        [HttpPost]
        public async Task<IActionResult> Editar(NegocioCreacionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string nombreArchivo = model.ImagenLogotipo;

            if (model.logotipo != null)
            {
                if (model.ImagenLogotipo != null)
                {
                    string rutaArchivo = Path.Combine(webHost.WebRootPath, "Imagenes", model.ImagenLogotipo);
                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }

                }

                nombreArchivo = subirImagen(model);
            }

            var negocio = new Negocio()
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Telefono = model.Telefono,
                Correo = model.Correo,
                Calle = model.Calle,
                Colonia = model.Colonia,
                Logotipo = nombreArchivo

            };

            unitOfWork.repositorioNegocio.actualizar(negocio);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Negocio");
        }

        private string subirImagen(NegocioCreacionViewModel model)
        {
            string nombreArchivo = "";

            string upload = Path.Combine(webHost.WebRootPath, "Imagenes");
            nombreArchivo = Guid.NewGuid().ToString() + "_" + model.logotipo.FileName;
            string rutaArchivo = Path.Combine(upload, nombreArchivo);
            model.logotipo.CopyTo(new FileStream(rutaArchivo, FileMode.Create));

            return nombreArchivo;

        }


    }
}
