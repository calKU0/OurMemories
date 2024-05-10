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
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Images.Include(i => i.Meeting);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // GET: Images/Create
        public IActionResult Create()
        {
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id");
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateImageViewModel imageVM)
        {
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
                    MeetingId = imageVM.MeetingId
                };

                _context.Add(image);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeetingId"] = new SelectList(_context.Meetings, "Id", "Id", imageVM.MeetingId);
            return View(imageVM);
        }

        // GET: Images/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
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
                MeetingId = image.MeetingId
            };
            return View(imageVM);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Url,MeetingId")] EditImageViewModel imageVM)
        {
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
                    MeetingId = imageVM.MeetingId
                };

                _context.Update(image);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Images/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Images
                .Include(i => i.Meeting)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                _context.Images.Remove(image);
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
