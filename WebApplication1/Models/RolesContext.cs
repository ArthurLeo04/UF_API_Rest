using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class RolesContext : DbContext
    {
        public RolesContext(DbContextOptions<RolesContext> options) : base(options)
        {
        }
        public DbSet<WebApplication1.Models.Roles> Roles { get; set; } = default!;
    }
}
