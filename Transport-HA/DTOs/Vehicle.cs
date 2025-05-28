using System.ComponentModel.DataAnnotations;

namespace Transport_HA.DTOs
{
    public enum FuelType
    {
        Gasoline,
        Hybrid,
        Electric
    }

    public record Vehicle(int Id, int PassengerCapacity, double Range, FuelType FuelType);
    public record VehicleAdd
    {
        [Required]
        public int PassengerCapacity { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Range must be greater than 0.")]
        public double Range { get; set; }
        [Required]
        [EnumDataType(typeof(FuelType), ErrorMessage = "Invalid fuel type.")]
        public FuelType FuelType { get; set; }
    }
}