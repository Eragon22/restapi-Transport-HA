using Microsoft.AspNetCore.Mvc;
using Transport_HA.DTOs;
using Transport_HA.Services;

namespace Transport_HA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Vehicle> GetVehicle(int id)
        {
            var vehicle = _vehicleService.Get(id);
            if (vehicle == null)
            {
                return NotFound($"Vehicle with ID {id} not found.");
            }
            return Ok(vehicle);
        }

        [HttpPost]
        public ActionResult<Vehicle> AddVehicle([FromBody] VehicleAdd vehicle)
        {
            if (vehicle == null)
            {
                return BadRequest("Vehicle data is required.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedVehicle = _vehicleService.Add(vehicle);
            return CreatedAtAction(nameof(GetVehicle), new { id = addedVehicle.Id }, addedVehicle);
        }
    }
}
