using ObiletCase.Models.Api.Journey;
using ObiletCase.Models.Api.Location;
using ObiletCase.Models.Api.Session;

namespace ObiletCase.Services
{
    public interface IObiletApiService
    {
        Task<SessionData> GetSessionAsync();
        Task<List<LocationData>> GetBusLocationsAsync(SessionData session, string? query = null);
        Task<List<JourneyItem>> GetJourneysAsync(SessionData session, int originId, int destinationId, DateTime departureDate);
    }
}
