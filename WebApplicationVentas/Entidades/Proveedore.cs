using System;
using System.Collections.Generic;

namespace WebApplicationVentas.Entidades;

public partial class Proveedore
{
    public int Id { get; set; }

    public int IdTipoDocumento { get; set; }

    public int IdRubro { get; set; }

    public string Nombre { get; set; }

    public string Apellidos { get; set; }

    public string Email { get; set; }

    public string Telefono { get; set; }

    public string Calle { get; set; }

    public string Colonia { get; set; }

    public string CodigoPostalCiudad { get; set; }

    public bool EsActivo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<EntradaProducto> EntradaProductos { get; set; } = new List<EntradaProducto>();

    public virtual Rubro IdRubroNavigation { get; set; }

    public virtual TiposDocumentosProvCliente IdTipoDocumentoNavigation { get; set; }
}
