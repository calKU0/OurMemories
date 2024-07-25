using System.Diagnostics;

namespace MemoriesWebApp.ViewModels
{
    public class EditImageViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public DateOnly Date { get; set; }
        public bool IsVisableForUser { get; set; }
        public IFormFile? Image { get; set; }
        public string Url { get; set; }
        public int MeetingId { get; set; }
    }
}
