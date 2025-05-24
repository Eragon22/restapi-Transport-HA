using Microsoft.AspNetCore.Mvc;
using Transport_HA.DTOs;
using Transport_HA.Services;

namespace Transport_HA.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> ListVehicles()
        {
            var vehicles = _vehicleService.List();
            return Ok(vehicles);
        }

        [HttpPost]
        public ActionResult<Vehicle> AddVehicle([FromBody] Vehicle vehicle)
        {
            var added = _vehicleService.Add(vehicle);
            return CreatedAtAction(nameof(AddVehicle), new { id = added.Id }, added);
        }
    }
}
