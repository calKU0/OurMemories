using Microsoft.AspNetCore.Mvc;

namespace MemoriesWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
