using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MemoriesWebApp.Data;
using MemoriesWebApp.Models;
using MemoriesWebApp.Interfaces;
using MemoriesWebApp.ViewModels;
using System.Diagnostics;

namespace MemoriesWebApp.Controllers
{
    public class ImagesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPhotoService _photoService;

        public ImagesController(AppDbContext context, IPhotoService photoService)
        {
            _context = context;
            this._photoService = photoService;
        }


        // GET: Images
        public async Task<IActionResult> Index(string sortOrder, string sortDirection)
        {
            var brows = Request.Headers["User-Agent"].ToString();
            bool isMobileDevice = brows != null && (
                brows.IndexOf("Mobile", StringComparison.OrdinalIgnoreCase) >= 0 ||
                brows.Contains("Android") ||
                brows.Contains("iPhone") ||
                brows.Contains("Windows Phone")
            );

            ViewBag.IsMobileDevice = isMobileDevice;

            sortOrder = string.IsNullOrEmpty(sortOrder) ? "MeetingId" : sortOrder;
            sortDirection = string.IsNullOrEmpty(sortDirection) ? "MeetingId" : sortDirection;

            ViewBag.SortDirection = sortDirection;
            ViewBag.CurrentSort = sortDirection;

            var images = from i in _context.Images.Include(i => i.Meeting) select i;

            // Apply sorting based on the direction
            if (!string.IsNullOrEmpty(sortDirection))
            {
                switch (sortDirection)
                {
                    case "Date_desc":
                        images = images.OrderByDescending(i => i.Date).ThenByDescending(i => i.Id);
                        break;
                    case "City_desc":
                        images = images.OrderByDescending(i => i.City);
                        break;
                    case "MeetingId_desc":
                        images = images.OrderByDescending(i => i.MeetingId).ThenByDescending(i => i.Id);
                        break;
                    default:
                        if (sortOrder == "Date")
                            images = images.OrderBy(i => i.Date);
                        else if (sortOrder == "City")
                            images = images.OrderBy(i => i.City);
                        else if (sortOrder == "MeetingId")
                            images = images.OrderBy(i => i.MeetingId);
                        else
                            images = images.OrderBy(i => i.MeetingId); // Default sorting
                        break;
                }
            }
            else
            {
                // Default sorting when sortDirection is not provided
                if (sortOrder == "Date")
                    images = images.OrderBy(i => i.Date);
                else if (sortOrder == "City")
                    images = images.OrderBy(i => i.City);
                else if (sortOrder == "MeetingId")
                    images = images.OrderBy(i => i.MeetingId);
                else
                    images = images.OrderBy(i => i.MeetingId); // Default sorting
            }

            return View(await images.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            var brows = Request.Headers["User-Agent"].ToString();
            bool isMobileDevice = brows != null && (
                brows.IndexOf("Mobile", StringComparison.OrdinalIgnoreCase) >= 0 ||
                brows.Contains("Android") ||
                brows.Contains("iPhone") ||
                brows.Contains("Windows Phone")
            );

            ViewBag.IsMobileDevice = isMobileDevice;

            var allImageIds = await _context.Images.Select(i => i.Id).ToListAsync();
            int currentIndex = allImageIds.IndexOf(id);

            int? previousId = currentIndex > 0 ? allImageIds[currentIndex - 1] : (int?)null;
            int? nextId = currentIndex < allImageIds.Count - 1 ? allImageIds[currentIndex + 1] : (int?)null;

            var viewModel = new ImageDetailsViewModel
            {
                Image = image,
                PreviousId = previousId,
                NextId = nextId
            };

            return View(viewModel);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id");

            var meetings = _context.Meetings
                .Select(m => new { m.Id, m.ImageUrl, m.MeetingCity, m.DateStart })
                .ToList();
            ViewBag.Meetings = meetings;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateImageViewModel imageVM)
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = _photoService.AddPhotoAsync(imageVM.Image);
                var image = new Image
                {
                    Name = imageVM.Name,
                    Description = imageVM.Description,
                    City = imageVM.City,
                    Date = imageVM.Date,
                    Url = result.Result.Url.ToString(),
                    IsVisableForUser = imageVM.IsVisableForUser,
                    MeetingId = imageVM.MeetingId
                };

                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var meetings = _context.Meetings
                .Select(m => new { m.Id, m.ImageUrl, m.MeetingCity, m.DateStart })
                .ToList();
            ViewBag.Meetings = meetings;

            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", imageVM.MeetingId);
            return View(imageVM);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            var imageVM = new EditImageViewModel
            {
                Name = image.Name,
                Description = image.Description,
                City = image.City,
                Date = image.Date,
                Url = image.Url,
                IsVisableForUser = image.IsVisableForUser,
                MeetingId = image.MeetingId
            };
            var meetings = _context.Meetings
                .Select(m => new { m.Id, m.ImageUrl, m.MeetingCity, m.DateStart })
                .ToList();
            ViewBag.Meetings = meetings;


            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id");
            return View(imageVM);
        }

        // POST: Images/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditImageViewModel imageVM)
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string url;
                if (imageVM.Image == null)
                {
                    url = imageVM.Url;
                }
                else
                {
                    var result = await _photoService.AddPhotoAsync(imageVM.Image);
                    url = result.Url.ToString();
                }

                var image = new Image
                {
                    Id = id,
                    Name = imageVM.Name,
                    Description = imageVM.Description,
                    City = imageVM.City,
                    Date = imageVM.Date,
                    Url = url,
                    IsVisableForUser = imageVM.IsVisableForUser,
                    MeetingId = imageVM.MeetingId
                };

                _context.Update(image);
                await _context.SaveChangesAsync();

                TempData["ShowModal"] = true;
            }

            var meetings = _context.Meetings
                .Select(m => new { m.Id, m.ImageUrl, m.MeetingCity, m.DateStart })
                .ToList();
            ViewBag.Meetings = meetings;

            return RedirectToAction(nameof(Index));
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);

                if (!string.IsNullOrEmpty(image.Url))
                {
                    _ = _photoService.DeletePhotoAsync(image.Url);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageExists(int id)
        {
            return _context.Images.Any(e => e.Id == id);
        }
    }
}
