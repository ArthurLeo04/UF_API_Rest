using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class FriendsContext : DbContext
    {
        public FriendsContext(DbContextOptions<FriendsContext> options) : base(options)
        {
        }
        public DbSet<Friends> Friends { get; set; } = default!;

        public DbSet<Users> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friends>()
                .HasKey(f => new { f.User1, f.User2 });
        }

    }
}
