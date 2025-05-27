using Transport_HA.DTOs;

namespace Transport_HA.Services
{
    public class SuggestionService : ISuggestionService
    {
        private readonly IVehicleService _vehicleService;

        public SuggestionService(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        private double CityKm(double distance)
        {
            return Math.Min(distance, 50);
        }

        private double HighwayKm(double distance)
        {
            return Math.Max(0, distance - 50);
        }

        private decimal CalculateTravelFee(Trip trip)
        {
            const decimal feePerKm = 2m;
            const decimal feePerHalfHour = 2m;

            double cityMinutes = CityKm(trip.Distance) * 2;
            double highwayMinutes = HighwayKm(trip.Distance) * 1;
            double totalMinutes = cityMinutes + highwayMinutes;

            int halfHours = (int)Math.Ceiling(totalMinutes / 30);

            decimal totalFee = (decimal)trip.Distance * feePerKm + halfHours * feePerHalfHour;
            return totalFee;
        }

        private decimal CalculateFuelCost(Trip trip, Vehicle vehicle)
        {
            double totalKm = CityKm(trip.Distance) + HighwayKm(trip.Distance);

            decimal cost = vehicle.FuelType switch
            {
                FuelType.Gasoline => 2m * (decimal)totalKm,
                FuelType.Electric => 1m * (decimal)totalKm,
                FuelType.Hybrid => 1 * (decimal)CityKm(trip.Distance) + 2 * (decimal)HighwayKm(trip.Distance),
                _ => throw new ArgumentOutOfRangeException()
            };

            return cost;
        }

        private decimal CalculateProfit(Trip trip, Vehicle vehicle)
        {
            decimal travelFee = CalculateTravelFee(trip);
            decimal fuelCost = CalculateFuelCost(trip, vehicle);

            return travelFee - fuelCost;
        }

        public IReadOnlyCollection<Suggestion> GenerateSuggestions(Trip trip)
        {
            var allVehicles = _vehicleService.List()
                            .Where(x => x.PassengerCapacity >= trip.PassengerCount)
                            .ToList();


            var HybridVehicles = allVehicles
                                .Where(x => x.Range >= Math.Round(CityKm(trip.Distance) / 2) + HighwayKm(trip.Distance) && x.FuelType == FuelType.Hybrid)
                                .ToList();

            var suggestedVehicles = allVehicles
                                    .Where(x => x.Range >= trip.Distance && x.FuelType != FuelType.Hybrid)
                                    .ToList();
            suggestedVehicles.AddRange(HybridVehicles);

            var vehicleProfits = suggestedVehicles
                .ToDictionary(vehicle => vehicle, vehicle => CalculateProfit(trip, vehicle));

            var sortedSuggestions = vehicleProfits
                .OrderByDescending(x => x.Value)
                .Select((x, index) => new Suggestion(index + 1, x.Value, x.Key))
                .ToList();

            return sortedSuggestions;
        }
    }
}
