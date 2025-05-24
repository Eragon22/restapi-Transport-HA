using Transport_HA.DAL;
using Transport_HA.Services;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Transport_HA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddScoped<IVehicleService, VehicleService>();
            builder.Services.AddDbContext<VehicleDbContext>(options =>
            options.UseInMemoryDatabase("VehicleDb"));
            builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            var app = builder.Build();

            app.MapControllers();

            app.Run();
        }
    }
}