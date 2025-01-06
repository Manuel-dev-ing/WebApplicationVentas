using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var registrosActivos = await unitOfWork.repositorioClientes.clientesActivos();
            var registrosInactivos = await unitOfWork.repositorioClientes.clientesInactivos();

            var modelo = new ClientesModel()
            {
                clientesActivos = registrosActivos,
                clientesInactivos = registrosInactivos
            };


            return View(modelo);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ClienteViewModel modelo)
        {

            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            var cliente = new Cliente()
            {
                Nombre = modelo.Nombre,
                Apellidos = modelo.Apellidos,
                Email = modelo.Email,
                Telefono = modelo.Telefono,
                Calle = modelo.Calle,
                Colonia = modelo.Colonia,
                CodigoPostalCiudad = modelo.CodigoPostalCiudad,
                EsActivo = true
            };


            unitOfWork.repositorioClientes.guardar(cliente);
            unitOfWork.Complete();

            return RedirectToAction("Index", "Clientes");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var existeCliente = await unitOfWork.repositorioClientes.existeCliente(id);

            if (!existeCliente)
            {
                return RedirectToAction("Index", "Clientes");
            }

            var cliente = await unitOfWork.repositorioClientes.obtenerClientePorId(id);

            var clienteViewModel = new ClienteViewModel()
            {
                Id = cliente.Id,
                Nombre = cliente.Nombre,
                Apellidos = cliente.Apellidos,
                Email = cliente.Email,
                Telefono = cliente.Telefono,
                Calle = cliente.Calle,
                Colonia = cliente.Colonia,
                CodigoPostalCiudad = cliente.CodigoPostalCiudad,
                Esactivo = cliente.EsActivo
            };


            return View(clienteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Actualizar(ClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var existeCliente = await unitOfWork.repositorioClientes.existeCliente(model.Id);

            if (!existeCliente)
            {
                return RedirectToAction("Index", "Clientes");
            }

            var cliente = new Cliente()
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Apellidos = model.Apellidos,
                Email = model.Email,
                Telefono = model.Telefono,
                Calle = model.Calle,
                Colonia = model.Colonia,
                CodigoPostalCiudad = model.CodigoPostalCiudad,
                EsActivo = model.Esactivo
            };

            unitOfWork.repositorioClientes.Actualizar(cliente);
            await unitOfWork.Complete();
            return RedirectToAction("Index", "Clientes");
        }

        public async Task<IActionResult> Eliminar(int id)
        {

            var existeCliente = await unitOfWork.repositorioClientes.existeCliente(id);

            if (!existeCliente)
            {
                return RedirectToAction("Index", "Clientes");
            }
            
            var cliente = await unitOfWork.repositorioClientes.obtenerClientePorId(id);
            cliente.EsActivo = false;

            unitOfWork.repositorioClientes.Eliminar(cliente);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Clientes");
        }

        public async Task<IActionResult> Restaurar(int id)
        {
            var existeCategoria = await unitOfWork.repositorioClientes.existeCliente(id);

            if (!existeCategoria)
            {
                return RedirectToAction("Index", "Clientes");
            }

            var modelo = await unitOfWork.repositorioClientes.obtenerClientePorId(id);

            modelo.EsActivo = true;
            unitOfWork.repositorioClientes.Actualizar(modelo);
            await unitOfWork.Complete();

            return RedirectToAction("Index", "Clientes");



        }


    }
}
