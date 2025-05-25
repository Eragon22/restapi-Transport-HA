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
            modelBuilder.Entity<DBVehicle>(entity =>
            {
                entity.Property(v => v.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(v => v.PassangerCapacity)
                    .IsRequired();

                entity.Property(v => v.Range)
                    .IsRequired();

                entity.Property(v => v.FuelType)
                    .IsRequired();
            });
        }
    }
}
