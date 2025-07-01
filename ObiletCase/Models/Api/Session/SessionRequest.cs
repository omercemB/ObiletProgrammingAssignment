using Newtonsoft.Json;

namespace ObiletCase.Models.Api.Session
{
    public class SessionRequest
    {
        public int Type { get; set; } = 7;

        public Connection Connection { get; set; } = new Connection
        {
            IpAddress = "165.114.41.21",
            Port = "5117"
        };

        public Browser Browser { get; set; } = new Browser
        {
            Name = "Chrome",
            Version = "114.0.0"
        };

        public ApplicationInfo Application { get; set; } = new ApplicationInfo
        {
            Version = "1.0.0.0",
            EquipmentId = "distribusion"
        };
    }

    public class Connection
    {
        [JsonProperty("ip-address")]
        public string IpAddress { get; set; }

        public string Port { get; set; }
    }

    public class Browser
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public class ApplicationInfo
    {
        public string Version { get; set; }

        [JsonProperty("equipment-id")]
        public string EquipmentId { get; set; }
    }

}
