using Newtonsoft.Json;

namespace ObiletCase.Models.Api.Journey
{
    public class JourneyResponse
    {
        public string Status { get; set; }
        public List<JourneyItem> Data { get; set; }
    }

    public class JourneyItem
    {
        public string Id { get; set; }

        [JsonProperty("partner-name")]
        public string PartnerName { get; set; }
        public JourneyDetail Journey { get; set; }

        [JsonProperty("internet-price")]
        public decimal InternetPrice { get; set; }
    }

    public class JourneyDetail
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public string Duration { get; set; }
    }
}
