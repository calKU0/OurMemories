using System.ComponentModel;
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
        public string? City { get; set; }
        public DateOnly Date  { get; set; }
        public string Url { get; set; }
        [DefaultValue(true)]
        public bool IsVisableForUser { get; set; }
        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }
        public Meeting? Meeting { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}