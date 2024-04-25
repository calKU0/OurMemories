namespace MemoriesWebApp.ViewModels
{
    public class CreateImageViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public DateOnly Date { get; set; }
        public IFormFile Url { get; set; }
        public int MeetingId { get; set; }

    }
}
