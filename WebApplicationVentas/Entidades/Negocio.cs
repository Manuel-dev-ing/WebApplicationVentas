using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class Negocio
{
    public int Id { get; set; }

    public string Logotipo { get; set; }

    public string Nombre { get; set; }

    public string Telefono { get; set; }

    public string Correo { get; set; }

    public string Calle { get; set; }

    public string Colonia { get; set; }
}
