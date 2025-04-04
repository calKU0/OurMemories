﻿using MemoriesWebApp.Data.Enum;
using System.Diagnostics;

namespace MemoriesWebApp.ViewModels
{
    public class EditMeetingViewModel
    {
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public MeetingCity MeetingCity { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public bool Realized { get; set; }
    }
}
