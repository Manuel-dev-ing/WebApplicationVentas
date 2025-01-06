namespace WebApplicationVentas.Models
{
    public class TiposDocumentoModel
    {
        public IEnumerable<TiposDocumentoViewModel> tiposDocumentosActivos { get; set; }
        public IEnumerable<TiposDocumentoViewModel> tiposDocuemntosInactivos { get; set; }

    }
}
