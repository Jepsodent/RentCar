using Microsoft.EntityFrameworkCore;
using RentCar.Models;

namespace RentCar.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}
        public DbSet<MsCar> MsCars { get; set; }
        public DbSet<MsCustomer> MsCustomers { get; set; }
        public DbSet<MsCarImages> MsCarImages { get; set; }
        public DbSet<MsEmployee> MsEmployees { get; set; }
        public DbSet<TrRental> TrRentals { get; set; }
        public DbSet<TrMaintenance> TrMaintenances { get; set; }
        public DbSet<LtPayment> LtPayments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MsCustomer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<MsCar>()
                .HasIndex(c => c.LicensePlate)
                .IsUnique();

            modelBuilder.Entity<MsEmployee>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
