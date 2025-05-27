namespace Transport_HA.DTOs
{
    public enum FuelType
    {
        Gasoline,
        Hybrid,
        Electric
    }

    public record Vehicle(int Id, int PassengerCapacity, double Range, FuelType FuelType);
    public record VehicleAdd(int PassengerCapacity, double Range, FuelType FuelType);

}
