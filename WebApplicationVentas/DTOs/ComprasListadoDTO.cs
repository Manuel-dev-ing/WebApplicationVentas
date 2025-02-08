namespace WebApplicationVentas.DTOs
{
    public class ComprasListadoDTO
    {
        public int Id { get; set; }
        public string Proveedor { get; set; }
        public string Almacen { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaRegistro { get; set; }



    }
}
