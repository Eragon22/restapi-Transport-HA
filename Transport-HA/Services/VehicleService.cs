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
                .Select(x => new Vehicle(x.Id, x.PassengerCapacity, x.Range, x.FuelType))
                .ToList();
        }

        public Vehicle? Get(int id)
        {
            var dbVehicle = _dbContext.Vehicles.Find(id);
            if (dbVehicle == null)
            {
                return null;
            }
            return new Vehicle
            (
                dbVehicle.Id,
                dbVehicle.PassengerCapacity,
                dbVehicle.Range,
                dbVehicle.FuelType
            );
        }

        public Vehicle Add(VehicleAdd vehicle)
        {
            var dbVehicle = new DBVehicle
            {
                Id = 0, 
                PassengerCapacity = vehicle.PassengerCapacity,
                Range = vehicle.Range,
                FuelType = vehicle.FuelType
            };

            _dbContext.Vehicles.Add(dbVehicle);
            _dbContext.SaveChanges();
            return new Vehicle
            (
                dbVehicle.Id,
                dbVehicle.PassengerCapacity,
                dbVehicle.Range,
                dbVehicle.FuelType
            );
        }
    }
}
