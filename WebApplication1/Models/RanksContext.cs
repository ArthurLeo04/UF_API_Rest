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
    }
}
