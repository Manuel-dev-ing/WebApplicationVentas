using Microsoft.AspNetCore.Mvc;
using WebApplicationVentas.Servicios;

namespace WebApplicationVentas.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
