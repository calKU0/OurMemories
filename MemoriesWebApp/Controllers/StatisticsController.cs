using MemoriesWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemoriesWebApp.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly AppDbContext _context;

        public StatisticsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var brows = Request.Headers["User-Agent"].ToString();
            bool isMobileDevice = brows != null && (
                brows.IndexOf("Mobile", StringComparison.OrdinalIgnoreCase) >= 0 ||
                brows.Contains("Android") ||
                brows.Contains("iPhone") ||
                brows.Contains("Windows Phone")
            );

            ViewBag.IsMobileDevice = isMobileDevice;

            var statistics = _context.GetStatistics();
            return View(statistics);
        }
    }
}
