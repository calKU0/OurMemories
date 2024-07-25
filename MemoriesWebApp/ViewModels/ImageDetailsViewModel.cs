using MemoriesWebApp.Models;

namespace MemoriesWebApp.ViewModels
{
    public class ImageDetailsViewModel
    {
        public Image Image { get; set; }
        public int? PreviousId{ get; set; }
        public int? NextId { get; set; }
    }
}
