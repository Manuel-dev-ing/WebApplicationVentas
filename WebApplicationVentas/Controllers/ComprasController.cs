using Microsoft.AspNetCore.Mvc;

namespace WebApplicationVentas.Controllers
{
    public class ComprasController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
    }
}
