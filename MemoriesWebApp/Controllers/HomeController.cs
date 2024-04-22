using MemoriesWebApp.Data;
using MemoriesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemoriesWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var meetingsList = new List<Meeting>();

            var upcomingMeeting = await _context.Meetings
                .Where(m => m.DateEnd >= DateTime.Now) 
                .OrderBy(m => m.DateStart) 
                .FirstOrDefaultAsync();

            var pastMeeting = await _context.Meetings
                .Where(m => m.DateEnd < DateTime.Now)
                .OrderByDescending(m => m.DateEnd) 
                .FirstOrDefaultAsync();

            meetingsList.Add(pastMeeting);
            meetingsList.Add(upcomingMeeting);

            return View(meetingsList);
        }
    }
}
