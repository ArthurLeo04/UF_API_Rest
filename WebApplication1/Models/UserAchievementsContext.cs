using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class UserAchievementsContext : DbContext
    {
        public UserAchievementsContext(DbContextOptions<UserAchievementsContext> options) : base(options)
        {
        }
        public DbSet<UserAchievements> UserAchievements { get; set; } = default!;

        public DbSet<Achievements> Achievements { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAchievements>()
                .HasKey(u => new { u.IdUser, u.IdAchievement });
        }
    }
}
