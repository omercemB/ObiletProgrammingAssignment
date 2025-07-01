using Newtonsoft.Json;
using ObiletCase.Models.Api.Journey;
using ObiletCase.Models.Api.Location;
using ObiletCase.Models.Api.Session;
using System.Net.Http.Headers;
using System.Text;

namespace ObiletCase.Services
{
    public class ObiletApiService : IObiletApiService // dependency injection ile bu servisi kullanacağız. Bu servis Obilet API'sine bağlanmak için HttpClient kullanacak ve tüm istekleri bu servis üzerinden yapacağız. Client tarafında istek atılmayacak.
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://v2-api.obilet.com/api/";
        private const string ApiToken = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";

        public ObiletApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", ApiToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<SessionData> GetSessionAsync()
        {
            var request = new SessionRequest();

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("client/getsession", content);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();

            var sessionResponse = JsonConvert.DeserializeObject<SessionResponse>(responseContent);

            return sessionResponse?.Data;
        }
        public async Task<List<LocationData>> GetBusLocationsAsync(SessionData session, string? query = null)
        {
            var request = new LocationRequest
            {
                Data = string.IsNullOrWhiteSpace(query) ? null : query,
                DeviceSession = session
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("location/getbuslocations", content);

            if (!response.IsSuccessStatusCode)
                return new List<LocationData>();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LocationResponse>(responseContent);

            return result?.Data ?? new List<LocationData>();
        }
        public async Task<List<JourneyItem>> GetJourneysAsync(SessionData session, int originId, int destinationId, DateTime departureDate)
        {
            var request = new JourneyRequest
            {
                DeviceSession = session,
                Data = new JourneyData
                {
                    OriginId = originId,
                    DestinationId = destinationId,
                    DepartureDate = departureDate.ToString("yyyy-MM-dd")
                }
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("journey/getbusjourneys", content);

            if (!response.IsSuccessStatusCode)
                return new List<JourneyItem>();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<JourneyResponse>(responseContent);

            return result?.Data ?? new List<JourneyItem>();
        }
    }
}
