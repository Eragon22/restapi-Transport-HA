using System.ComponentModel.DataAnnotations;

namespace Transport_HA.DTOs
{
    public record Trip
    {
        [Range(1, int.MaxValue, ErrorMessage = "Passenger count must be at least 1.")]
        public int PassengerCount { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Distance must be greater than 0.")]
        public double Distance { get; set; }
    }
}
