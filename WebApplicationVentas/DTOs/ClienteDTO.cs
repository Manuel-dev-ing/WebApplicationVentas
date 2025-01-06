namespace WebApplicationVentas.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string Email { get; set; }

        public string Telefono { get; set; }

        public string Calle { get; set; }

        public string Colonia { get; set; }

        public string CodigoPostalCiudad { get; set; }

        public bool EsActivo { get; set; }


    }
}
