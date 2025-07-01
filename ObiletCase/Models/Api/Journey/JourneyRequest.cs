using Newtonsoft.Json;
using ObiletCase.Models.Api.Session;

namespace ObiletCase.Models.Api.Journey
{
    public class JourneyRequest
    {
        [JsonProperty("device-session")]
        public SessionData DeviceSession { get; set; }

        public string Date => DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        public string Language { get; set; } = "tr-TR";

        public JourneyData Data { get; set; }
    }

    public class JourneyData
    {
        [JsonProperty("origin-id")]
        public int OriginId { get; set; }

        [JsonProperty("destination-id")]
        public int DestinationId { get; set; }

        [JsonProperty("departure-date")]
        public string DepartureDate { get; set; }
    }
}
