namespace WebApplicationVentas.DTOs
{
    public class VentasListadoDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Cliente { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
    }
}
