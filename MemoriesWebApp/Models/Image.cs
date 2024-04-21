using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriesWebApp.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string Url { get; set; }
        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }
        public Meeting? Meeting { get; set; }
    }
}