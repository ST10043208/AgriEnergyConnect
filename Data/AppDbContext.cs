using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AgriEnergyConnect.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Farmer> Farmers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.ApplicationUser)
                .WithMany()
                .HasForeignKey(e => e.Id);
            modelBuilder.Entity<Farmer>()
                .HasOne(e => e.ApplicationUser)
                .WithMany()
                .HasForeignKey(e => e.Id);
            modelBuilder.Entity<Product>()
                .HasOne(e => e.Farmer)
                .WithMany()
                .HasForeignKey(e => e.FarmerId);
            
        }
    }
}
