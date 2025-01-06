using Microsoft.AspNetCore.Mvc;

namespace WebApplicationVentas.Controllers
{

    [ApiController]
    [Route("api/principal")]
    public class ApiController : ControllerBase
    {
        [HttpPost]
        public IActionResult Index(int numero)
        {
            try
            {
                // Procesa la venta aquí
                return Ok(new { mensaje = "Venta realizada con éxito" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Ocurrió un error interno", detalle = ex.Message });
            }
        }
    }
}
