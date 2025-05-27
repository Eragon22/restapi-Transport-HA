using Transport_HA.DTOs;

namespace Transport_HA.Services
{
    public interface ISuggestionService
    {
        public IReadOnlyCollection<Suggestion> GenerateSuggestions(Trip trip);
    }
}
