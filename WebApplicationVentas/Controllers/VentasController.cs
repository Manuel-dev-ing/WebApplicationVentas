using Microsoft.AspNetCore.Mvc;

namespace WebApplicationVentas.Controllers
{
    public class VentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //crear
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }





    }
}
