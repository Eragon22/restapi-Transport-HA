using Microsoft.AspNetCore.Mvc;
using Transport_HA.Services;

namespace Transport_HA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuggestionController : ControllerBase
    {
        private readonly ISuggestionService _suggestionService;

        public SuggestionController(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        [HttpPost]
        public ActionResult<IEnumerable<DTOs.Suggestion>> GenerateSuggestions([FromBody] DTOs.Trip trip)
        {
            if (trip == null)
            {
                return BadRequest("Trip data is required.");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var suggestions = _suggestionService.GenerateSuggestions(trip);

            if (suggestions == null || !suggestions.Any())
            {
                return Ok(Enumerable.Empty<DTOs.Suggestion>());
            }
            return Ok(suggestions);
        }
    }
}
