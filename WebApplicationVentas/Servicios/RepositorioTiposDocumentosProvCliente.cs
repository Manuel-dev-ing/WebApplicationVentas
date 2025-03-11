using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationVentas.Entidades;
using WebApplicationVentas.Models;

namespace WebApplicationVentas.Servicios
{
    public interface IRepositorioTiposDocumentosProvCliente
    {
        void actualizar(TiposDocumentosProvCliente documentosProvCliente);
        int contarElementos();
        int contarElementosInactivos();
        Task<bool> existeRegistro(int id);
        void guardar(TiposDocumentosProvCliente documentosProvCliente);
        Task<IEnumerable<TiposDocumentoViewModel>> ListadoregistrosActivos();
        Task<TiposDocumentosProvCliente> obtenerTiposDocumentos(int id);
        Task<IEnumerable<TiposDocumentoViewModel>> registrosActivos(PaginacionViewModel paginacion);
        Task<IEnumerable<TiposDocumentoViewModel>> registrosInactivos(PaginacionViewModel paginacion);
    }

    public class RepositorioTiposDocumentosProvCliente: IRepositorioTiposDocumentosProvCliente
    {
        private readonly ApplicationDbContext context;

        public RepositorioTiposDocumentosProvCliente(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int contarElementos()
        {
            var resultado = context.TiposDocumentosProvClientes.Where(x => x.EsActivo == true).Count();
            return resultado;
        }

        public int contarElementosInactivos()
        {
            var resultado = context.TiposDocumentosProvClientes.Where(x => x.EsActivo == false).Count();
            return resultado;
        }


        public async Task<IEnumerable<TiposDocumentoViewModel>> ListadoregistrosActivos()
        {
            var tiposDocumentos = await context.TiposDocumentosProvClientes
                .Where(x => x.EsActivo == true)
                .Select(a => new TiposDocumentoViewModel()
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.FechaRegistro

                }).ToListAsync();

            return tiposDocumentos;
        }

        public async Task<IEnumerable<TiposDocumentoViewModel>> registrosActivos(PaginacionViewModel paginacion)
        {
            var tiposDocumentos = await context.TiposDocumentosProvClientes
                .Where(x => x.EsActivo == true)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new TiposDocumentoViewModel()
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.FechaRegistro

                }).ToListAsync();

            return tiposDocumentos;
        }

        public async Task<IEnumerable<TiposDocumentoViewModel>> registrosInactivos(PaginacionViewModel paginacion)
        {
            var tiposDocumentos = await context.TiposDocumentosProvClientes
                .Where(x => x.EsActivo == false)
                .OrderBy(x => x.Id)
                .Skip(paginacion.RecordsASaltar)
                .Take(paginacion.RecordsPorPagina)
                .Select(a => new TiposDocumentoViewModel()
                {
                    Id = a.Id,
                    Descripcion = a.Descripcion,
                    Fecha = a.FechaRegistro

                }).ToListAsync();

            return tiposDocumentos;
        }

        public void guardar(TiposDocumentosProvCliente documentosProvCliente)
        {
            context.TiposDocumentosProvClientes.Add(documentosProvCliente);
        }

        public void actualizar(TiposDocumentosProvCliente documentosProvCliente)
        {
            context.TiposDocumentosProvClientes.Update(documentosProvCliente);
        }

        public async Task<bool> existeRegistro(int id)
        {
            var existeRegistro = await context.TiposDocumentosProvClientes.AnyAsync(x => x.Id == id);
            return existeRegistro;
        }

        public async Task<TiposDocumentosProvCliente> obtenerTiposDocumentos(int id)
        {
            var entidad = await context.TiposDocumentosProvClientes.FirstOrDefaultAsync(x => x.Id == id);

            return entidad;
        }
        


    }
}
