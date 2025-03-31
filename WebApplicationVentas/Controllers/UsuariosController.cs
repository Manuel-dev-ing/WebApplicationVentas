using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;

        public UsuariosController(IUnitOfWork unitOfWork, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(PaginacionViewModel paginacion)
        {

            var usuariosActivos = await unitOfWork.repositorioUsuarios.usuariosActivos(paginacion);
            var total = unitOfWork.repositorioUsuarios.contarElementos();

            var modelo = new PaginacionRespuesta<UsuariosViewModel>()
            {
                ElementosActivos = usuariosActivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/Usuarios"
            };

            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> ElementosInactivos(PaginacionViewModel paginacion)
        {
            var usuariosInactivos = await unitOfWork.repositorioUsuarios.usuariosInactivos(paginacion);
            var total = unitOfWork.repositorioUsuarios.contarElementos();

            var modelo = new PaginacionRespuesta<UsuariosViewModel>()
            {
                ElementosInactivos = usuariosInactivos,
                Pagina = paginacion.Pagina,
                RecordsPorPagina = paginacion.RecordsPorPagina,
                CantidadTotalRecords = total,
                BaseURL = "/Usuarios/ElementosInactivos"
            };

            return View(modelo);
        }


        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var modelo = new UsuariosCreacionViewModel();
            modelo.tiposRol = await obtenerTiposRol();


            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(UsuariosCreacionViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.tiposRol = await obtenerTiposRol();
                return View(model);
            }

            var usuario = new Usuario()
            {
                IdRol = model.IdRol,
                Nombre = model.Nombre,
                Apellidos = model.Apellidos,
                Correo = model.Correo,
                Telefono = model.Telefono,
                EsActivo = true,
                FechaRegistro = DateTime.UtcNow
            };

            var resultado = await userManager.CreateAsync(usuario, password: model.Password);
            

            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Usuarios");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                model.tiposRol = await obtenerTiposRol();
                return View(model);

            }


        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            var existeUsuario = await unitOfWork.repositorioUsuarios.existeUsuario(id);

            if (!existeUsuario)
            {
                return RedirectToAction("Index", "Usuarios");
            }

            var usuario = await unitOfWork.repositorioUsuarios.obtenerPorId(id);


            var modelo = new UsuariosEditarViewModel()
            {
                Id = usuario.Id,
                IdRol = usuario.IdRol,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                EsActivo = usuario.EsActivo,
                tiposRol = await obtenerTiposRol()

            };

            return View(modelo);

        }

        [HttpPost]
        public async Task<IActionResult> Editar(UsuariosEditarViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.tiposRol = await obtenerTiposRol();
                return View(model);
            }

            var usuario = new Usuario()
            {
                Id = model.Id,
                IdRol = model.IdRol,
                Nombre = model.Nombre,
                Apellidos = model.Apellidos,
                Correo = model.Correo,
                Telefono = model.Telefono,
                EsActivo = model.EsActivo,
                FechaRegistro = DateTime.UtcNow
            };

            unitOfWork.repositorioUsuarios.actualizar(usuario);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Usuarios");

        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var existeUsuario = await unitOfWork.repositorioUsuarios.existeUsuario(id);

            if (!existeUsuario)
            {
                return RedirectToAction("Index", "Usuarios");
            }

            var usuario = await unitOfWork.repositorioUsuarios.obtenerPorId(id);
            usuario.EsActivo = false;

            unitOfWork.repositorioUsuarios.actualizar(usuario);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Usuarios");
        }
        [HttpGet]
        public async Task<IActionResult> Restaurar(int id)
        {
            var existeUsuario = await unitOfWork.repositorioUsuarios.existeUsuario(id);

            if (!existeUsuario)
            {
                return RedirectToAction("Index", "Usuarios");
            }

            var usuario = await unitOfWork.repositorioUsuarios.obtenerPorId(id);
            usuario.EsActivo = true;

            unitOfWork.repositorioUsuarios.actualizar(usuario);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Usuarios");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var resultado = await signInManager.PasswordSignInAsync(model.Correo, model.Password, isPersistent: false, lockoutOnFailure: false);


            if (resultado.Succeeded) {


                return RedirectToAction("Index", "Dashboard");
            
            }
            else
            {
                ModelState.AddModelError(String.Empty, "Nombre de usuario o password incorrecto");
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Usuarios");
        }

        private async Task<IEnumerable<SelectListItem>> obtenerTiposRol()
        {
            var tiposRol = await unitOfWork.repositorioRol.Listadorol();
            var resultado = tiposRol.Select(x => new SelectListItem(x.Descripcion, x.Id.ToString())).ToList();

            var opcionPorDefecto = new SelectListItem("-- Seleccione un Usuario --", "0", true);
            resultado.Insert(0, opcionPorDefecto);

            return resultado;
        }
    }
}
