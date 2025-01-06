using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioClientes
    {
        void Actualizar(Cliente cliente);
        Task<IEnumerable<ClienteViewModel>> clientesActivos();
        Task<IEnumerable<ClienteViewModel>> clientesInactivos();
        void Eliminar(Cliente cliente);
        Task<bool> existeCliente(int id);
        void guardar(Cliente cliente);
        Task<Cliente> obtenerClientePorId(int id);
    }

    public class RepositorioClientes: IRepositorioClientes
    {
        private readonly ApplicationDbContext context;

        public RepositorioClientes(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ClienteViewModel>> clientesActivos()
        {

            var clientes = await context.Clientes.Where(x => x.EsActivo == true).Select(a => new ClienteViewModel
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellidos = a.Apellidos,
                Email = a.Email,
                Telefono = a.Telefono,
                Calle = a.Calle,
                Colonia = a.Colonia,
                CodigoPostalCiudad = a.CodigoPostalCiudad,
                Esactivo = a.EsActivo

            }).ToListAsync();


            return clientes;
        }

        public async Task<IEnumerable<ClienteViewModel>> clientesInactivos()
        {

            var clientes = await context.Clientes.Where(x => x.EsActivo == false).Select(a => new ClienteViewModel
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellidos = a.Apellidos,
                Email = a.Email,
                Telefono = a.Telefono,
                Calle = a.Calle,
                Colonia = a.Colonia,
                CodigoPostalCiudad = a.CodigoPostalCiudad,
                Esactivo = a.EsActivo

            }).ToListAsync();


            return clientes;
        }

        public void guardar(Cliente cliente)
        {
            context.Clientes.Add(cliente);
        }

        public void Actualizar(Cliente cliente)
        {
            context.Clientes.Update(cliente);
        }
        public void Eliminar(Cliente cliente)
        {
            context.Clientes.Update(cliente);
        }

        public async Task<bool> existeCliente(int id)
        {
            var existeCliente = await context.Clientes.AnyAsync(x => x.Id == id);

            return existeCliente;
        }


        public async Task<Cliente> obtenerClientePorId(int id)
        {

            var cliente = await context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            return cliente;

        }





    }
}
