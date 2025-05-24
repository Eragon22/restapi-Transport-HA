using Microsoft.EntityFrameworkCore;
using Transport_HA.DAL.Entities;
using Transport_HA.DTOs;

namespace Transport_HA.DAL
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {
        }
        public DbSet<DBVehicle> Vehicles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBVehicle>()
                .HasKey(v => v.Id);

            modelBuilder.Entity<DBVehicle>()
                .Property(v => v.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
