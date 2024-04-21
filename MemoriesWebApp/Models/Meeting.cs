using MemoriesWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MemoriesWebApp.Models
{
    public class Meeting
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public MeetingCity MeetingCity { get; set; }
        public bool Realized { get; set; }
    }
}
