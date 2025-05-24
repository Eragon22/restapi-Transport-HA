using Transport_HA.DTOs;

namespace Transport_HA.DAL.Entities
{
    public class DBVehicle
    {
        public int Id { get; set; }
        public int PassangerCapacity { get; set; }
        public double Range { get; set; }
        public required FuelType FuelType { get; set; }
    }
}
