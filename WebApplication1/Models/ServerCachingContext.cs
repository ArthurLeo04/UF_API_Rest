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
        public DbSet<Ranks> Ranks { get; set; } = default!; // To get rank for player

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key for Ranks
            modelBuilder.Entity<Ranks>()
                .HasKey(r => r.Rank);
        }
    }
}
