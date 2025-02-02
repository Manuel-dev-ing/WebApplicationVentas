using System.Linq.Expressions;
using WebApplicationVentas.Entidades;

namespace WebApplicationVentas.Servicios
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositorioUsuarios repositorioUsuarios { get; }
        IRepositorioAlmacenes repositorioAlmacenes { get; }
        IRepositorioCategorias repositorioCategorias { get; }
        IRepositorioClientes repositorioClientes { get; }
        IRepositorioMarcas repositorioMarcas { get; }
        IRepositorioRubros repositorioRubros { get; }
        IRepositorioProductos repositorioProductos { get; }
        IRepositorioTiposDocumentosProvCliente repositorioTiposDocumentosProv { get; }
        IRepositorioProveedores repositorioProveedores { get; }
        IRepositorioRol repositorioRol { get; }
        IRepositorioVentas repositorioVentas { get; }
        IRepositorioCompras repositorioCompras { get; }
        IRepositorioStockProductos repositorioStockProductos { get; }

        Task<int> Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IRepositorioUsuarios repositorioUsuarios { get; private set; }
        public IRepositorioAlmacenes repositorioAlmacenes { get; private set; }
        public IRepositorioCategorias repositorioCategorias { get; private set; }
        public IRepositorioClientes repositorioClientes { get; private set; }
        public IRepositorioMarcas repositorioMarcas { get; private set; }
        public IRepositorioRubros repositorioRubros { get; private set; }
        public IRepositorioProductos repositorioProductos { get; private set; }
        public IRepositorioTiposDocumentosProvCliente repositorioTiposDocumentosProv { get; private set; }
        public IRepositorioProveedores repositorioProveedores { get; private set; }

        public IRepositorioRol repositorioRol { get; private set; }

        public IRepositorioVentas repositorioVentas { get; private set; }

        public IRepositorioCompras repositorioCompras { get; private set; }

        public IRepositorioStockProductos repositorioStockProductos { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            repositorioUsuarios = new RepositorioUsuarios(context);
            repositorioAlmacenes = new RepositoriosAlmacenes(context);
            repositorioCategorias = new RepositorioCategorias(context);
            repositorioClientes = new RepositorioClientes(context);
            repositorioMarcas = new RepositorioMarcas(context);
            repositorioRubros = new RepositorioRubros(context);
            repositorioProductos = new RepositorioProductos(context);
            repositorioTiposDocumentosProv = new RepositorioTiposDocumentosProvCliente(context);
            repositorioProveedores = new RepositorioProveedores(context);
            repositorioRol = new RepositorioRol(context);
            repositorioVentas = new RepositorioVentas(context);
            repositorioCompras = new RepositorioCompras(context);
            repositorioStockProductos = new RepositorioStockProductos(context);
        }

        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();// Guarda todos los cambios
        }

        public void Dispose()
        {
            context.Dispose();// Libera los recursos
        }
    }
}
