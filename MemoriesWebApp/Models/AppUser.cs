using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MemoriesWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Image> Images { get; set; }
        public ICollection<Meeting> Meetings { get; set; }
    }
}
