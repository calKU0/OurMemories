using MemoriesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoriesWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
