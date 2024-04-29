using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class FriendRequestsContext : DbContext
    {
        public FriendRequestsContext(DbContextOptions<FriendRequestsContext> options) : base(options)
        {
        }
        
        public DbSet<FriendRequests> FriendRequests { get; set; } = default!;

        public DbSet<Users> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendRequests>()
                .HasKey(f => new { f.Sender, f.Receiver });
        }

    }
}
