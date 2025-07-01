using Newtonsoft.Json;
using ObiletCase.Models.Api.Session;

namespace ObiletCase.Models.Api.Location
{
    public class LocationRequest
    {
        [JsonProperty("data")]
        public string? Data { get; set; } = null;

        [JsonProperty("device-session")]
        public SessionData DeviceSession { get; set; }

        public string Date => DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss");
        public string Language { get; set; } = "tr-TR";
    }
}
