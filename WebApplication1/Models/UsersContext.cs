using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; } = default!;

        public DbSet<UserAchievements> UserAchievements { get; set; } = default!;

        public DbSet<Achievements> Achievements { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key for UserAchievements
            modelBuilder.Entity<UserAchievements>()
                .HasKey(ua => new { ua.IdUser, ua.IdAchievement });
        }
    }
}
