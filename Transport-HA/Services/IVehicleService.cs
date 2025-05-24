using Transport_HA.DTOs;

namespace Transport_HA.Services
{
    public interface IVehicleService
    {
        IReadOnlyCollection<Vehicle> List();
        Vehicle Add(Vehicle vehicle);
    }
}
