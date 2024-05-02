using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Achievements> Achievements { get; set; } // Add DbSet for the Achievements entity
        // Add DbSet properties for other entities if necessary

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().ToTable("users");
            modelBuilder.Entity<Roles>().ToTable("roles");
            modelBuilder.Entity<Achievements>().ToTable("achievements");
            // Configure other entity mappings and relationships

            // Seed default roles
            modelBuilder.Entity<Roles>().HasData(
                new Roles { Role = "client" },
                new Roles { Role = "server" }
            );

            // Set primary key for roles
            modelBuilder.Entity<Roles>().HasKey(r => r.Role);
        }
    }
}