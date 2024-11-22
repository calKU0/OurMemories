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
using Microsoft.IdentityModel.Tokens;

namespace MemoriesWebApp.Controllers
{
    public class MeetingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPhotoService _photoService;

        public MeetingController(AppDbContext context, IPhotoService photoService)
        {
            _context = context;
            this._photoService = photoService;        
        }

        // GET: Meeting
        public async Task<IActionResult> Index()
        {
            return View(await _context.Meetings.ToListAsync());
        }

        // GET: Meeting/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            var images = await _context.Images
                .Where(i => i.MeetingId == id)
                .ToListAsync();

            ViewBag.Images = images;


            return View(meeting);
        }

        // GET: Meeting/Create
        public IActionResult Create()
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            TempData["ShowModal"] = false;
            return View();
        }

        // POST: Meeting/Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateMeetingViewModel meetingVM)
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var result = meetingVM.Image == null ? null :_photoService.AddPhotoAsync(meetingVM.Image);
                var meeting = new Meeting
                {
                    MeetingCity = meetingVM.MeetingCity,
                    DateStart = meetingVM.DateStart,
                    DateEnd = meetingVM.DateEnd,
                    Realized = meetingVM.Realized,
                    Description = meetingVM.Description,
                    ImageUrl = result != null ? result.Result.Url.ToString() : "https://i.imgur.com/clR6N7I.png"
                };

                _context.Add(meeting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(meetingVM);
        }

        // GET: Meeting/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            TempData["ShowModal"] = false;
            if (id == null)
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.FindAsync(id);

            var meetingVM = new EditMeetingViewModel
            {
                DateStart = meeting.DateStart,
                DateEnd = meeting.DateEnd,
                MeetingCity = meeting.MeetingCity,
                Url = meeting.ImageUrl,
                Description = meeting.Description,
                Realized = meeting.Realized,
            };
            return View(meetingVM);
        }

        // POST: Meeting/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditMeetingViewModel meetingVM)
        {
            if (!(User.IsInRole("admin") || User.IsInRole("superuser")))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string url;
                if (meetingVM.Image == null)
                {
                    url = meetingVM.Url;
                }
                else
                {
                    var result = await _photoService.AddPhotoAsync(meetingVM.Image);
                    url = result.Url.ToString();
                }

                var meeting = new Meeting
                {
                    Id = id,
                    DateStart = meetingVM.DateStart,
                    DateEnd = meetingVM.DateEnd,
                    MeetingCity = meetingVM.MeetingCity,
                    Description = meetingVM.Description,
                    ImageUrl = url,
                    Realized = meetingVM.Realized,
                };

                _context.Update(meeting);
                await _context.SaveChangesAsync();

                TempData["ShowModal"] = true;

            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Meeting/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!User.IsInRole("admin") || !User.IsInRole("superuser"))
            {
                return NotFound();
            }

            var meeting = await _context.Meetings.FindAsync(id);
            var meetingImages = await _context.Images
                .Where(i => i.MeetingId == id)
                .ToListAsync();

            if (meeting != null)
            {
                _context.Meetings.Remove(meeting);
                if (!string.IsNullOrEmpty(meeting.ImageUrl))
                {
                    _ = _photoService.DeletePhotoAsync(meeting.ImageUrl);
                }

                if (meetingImages != null)
                {
                    foreach (var image in meetingImages)
                    {
                        _ = _photoService.DeletePhotoAsync(image.Url);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingExists(int id)
        {
            return _context.Meetings.Any(e => e.Id == id);
        }
    }
}
