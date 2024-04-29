using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class RanksContext : DbContext
    {
        public RanksContext(DbContextOptions<RanksContext> options) : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Ranks> Ranks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set primary key for Ranks
            modelBuilder.Entity<Ranks>()
                .HasKey(r => r.Rank);
        }
    }
}
