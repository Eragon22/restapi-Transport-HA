namespace Transport_HA.DTOs
{
    public enum FuelType
    {
        Gasoline,
        Hybrid,
        Electric
    }

    public record Vehicle(int PassangerCapacity, double Range, FuelType FuelType);
}
