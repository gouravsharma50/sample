using Microsoft.AspNetCore.Mvc;

namespace SampleWebApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
