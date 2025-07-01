namespace ObiletCase.Models.Api.Session
{
    public class SessionResponse
    {
        public string Status { get; set; }
        public SessionData Data { get; set; }
    }

    public class SessionData
    {
        [Newtonsoft.Json.JsonProperty("session-id")]
        public string SessionId { get; set; }

        [Newtonsoft.Json.JsonProperty("device-id")]
        public string DeviceId { get; set; }
    }
}
