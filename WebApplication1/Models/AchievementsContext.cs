using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class AchievementsContext : DbContext
    {
        public AchievementsContext(DbContextOptions<AchievementsContext> options) : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Achievements> Achievements { get; set; } = default!;
    }
}
