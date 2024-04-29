using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class ServerCachingContext : DbContext
    {
        public ServerCachingContext(DbContextOptions<ServerCachingContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; } = default!; // To get match for player
    }
}
