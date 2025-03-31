using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioUsuarios
    {
        void actualizar(Usuario usuario);
        Task<Usuario> buscarPorId(Usuario usuario);
        Task<Usuario> buscarUsuarioPorCorreo(string correo);
        int contarElementos();
        int contarElementosInactivos();
        void crearUsuario(Usuario usuario);
        Task<bool> existeUsuario(int id);
        Task<Usuario> obtenerPorId(int id);
        Task<IEnumerable<UsuariosViewModel>> usuariosActivos(PaginacionViewModel paginacion);
        Task<IEnumerable<UsuariosViewModel>> usuariosInactivos(PaginacionViewModel paginacion);
    }


    public class RepositorioUsuarios:IRepositorioUsuarios
    {
        private readonly ApplicationDbContext context;

        public RepositorioUsuarios(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int contarElementos()
        {
            var resultado = context.Usuarios.Where(x => x.EsActivo == true).Count();
            return resultado;
        }

        public int contarElementosInactivos()
        {
            var resultado = context.Usuarios.Where(x => x.EsActivo == false).Count();
            return resultado;
        }

        public async Task<IEnumerable<UsuariosViewModel>> usuariosActivos(PaginacionViewModel paginacion)
        {
            var entidad = await context.Usuarios
                .Where(x => x.EsActivo == true)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new UsuariosViewModel()
            {
                Id = a.Id,
                NombreRol = a.IdRolNavigation.Descripcion,
                Nombre = a.Nombre,
                Apellidos = a.Apellidos,
                Correo = a.Correo,
                Telefono = a.Telefono
            
            }).ToListAsync();

            return entidad;
        }


        public async Task<IEnumerable<UsuariosViewModel>> usuariosInactivos(PaginacionViewModel paginacion)
        {
            var entidad = await context.Usuarios
                .Where(x => x.EsActivo == false)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new UsuariosViewModel()
            {
                Id = a.Id,
                NombreRol = a.IdRolNavigation.Descripcion,
                Nombre = a.Nombre,
                Apellidos = a.Apellidos,
                Correo = a.Correo,
                Telefono = a.Telefono

            }).ToListAsync();

            return entidad;
        }

        public async Task<Usuario> obtenerPorId(int id)
        {
            var entidad = await context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            return entidad;
        }

        public async Task<bool> existeUsuario(int id)
        {
            var entidad = await context.Usuarios.AnyAsync(x => x.Id == id);
            return entidad;
        }

        public void actualizar(Usuario usuario)
        {
            context.Usuarios.Update(usuario);
        }

        public void crearUsuario(Usuario usuario)
        {
            context.Usuarios.Add(usuario);
        }


        public async Task<Usuario> buscarPorId(Usuario usuario)
        {
            var entidad = await context.Usuarios.FirstOrDefaultAsync(x=> x.Id == usuario.Id);
            return entidad;
        }


        public async Task<Usuario> buscarUsuarioPorCorreo(string correo)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Correo == correo);
            return usuario;
        }



    }
}
