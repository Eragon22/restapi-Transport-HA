using Transport_HA.DTOs;

namespace Transport_HA.Services
{
    public interface IVehicleService
    {
        IReadOnlyCollection<Vehicle> List();

        Vehicle? Get(int id);

        Vehicle Add(VehicleAdd vehicle);
    }
}
