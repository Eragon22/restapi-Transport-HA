using Microsoft.EntityFrameworkCore;
using Transport_HA.DAL;
using Transport_HA.DAL.Entities;
using Transport_HA.DTOs;

namespace Transport_HA.Services
{
    public class VehicleService : IVehicleService
    {
        public readonly VehicleDbContext _dbContext;

        public VehicleService(VehicleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyCollection<Vehicle> List()
        {
            return _dbContext.Set<DBVehicle>()
                .Select(v => new Vehicle(v.Id, v.PassangerCapacity, v.Range, v.FuelType))
                .ToList();
        }

        public Vehicle Add(Vehicle vehicle)
        {
            var dbVehicle = new DBVehicle
            {
                Id = vehicle.Id,
                PassangerCapacity = vehicle.PassangerCapacity,
                Range = vehicle.Range,
                FuelType = vehicle.FuelType
            };

            _dbContext.Vehicles.Add(dbVehicle);
            _dbContext.SaveChanges();
            return vehicle;
        }
    }
}
