using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Transport_HA.DAL;
using Transport_HA.DAL.Entities;
using Transport_HA.DTOs;
using Transport_HA.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IVehicleService, VehicleService>();
        builder.Services.AddScoped<ISuggestionService, SuggestionService>();

        builder.Services.AddDbContext<VehicleDbContext>(options =>
            options.UseInMemoryDatabase("VehicleDb"));

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<VehicleDbContext>();

            // Seed the database with initial data for testing
            dbContext.Vehicles.AddRange(
                new DBVehicle { Id = 1, PassengerCapacity = 5, Range = 400.0, FuelType = FuelType.Gasoline },
                new DBVehicle { Id = 2, PassengerCapacity = 4, Range = 300.0, FuelType = FuelType.Hybrid },
                new DBVehicle { Id = 3, PassengerCapacity = 2, Range = 250.0, FuelType = FuelType.Electric }
            );
            dbContext.SaveChanges();
        }

        app.MapControllers();

        app.Run();
    }
}
