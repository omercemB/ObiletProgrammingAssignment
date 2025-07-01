using Newtonsoft.Json;

namespace ObiletCase.Models.Api.Location
{
    public class LocationResponse
    {
        public string Status { get; set; }
        public List<LocationData> Data { get; set; }
    }

    public class LocationData
    {
        public int Id { get; set; }

        [JsonProperty("parent-id")]
        public int? ParentId { get; set; }

        public string Name { get; set; }

        [JsonProperty("geo-location")]
        public GeoLocation GeoLocation { get; set; }
    }

    public class GeoLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Zoom { get; set; }
    }
}
