using ObiletCase.Models.Api.Session;
using System.Text.Json;

namespace ObiletCase.Infrastructure.Helpers
{
    public static class SessionManager //Burada session yönetimi için bir yardımcı sınıf oluşturuyoruz. Amaç session verilerini yönetmek ve süresini kontrol etmek.
                                       // ayrıca sessionu cacheleyerek her seferde API'ye istek göndermemek.
    {
        private const string SessionKey = "ObiletSession";
        private const string TimestampKey = "ObiletSessionTimestamp";
        private static readonly TimeSpan Expiration = TimeSpan.FromMinutes(25); // Obilet session timeouta göre ayarlanabilir. burası olmayadabilirdi

        public static SessionData? Get(HttpContext context)
        {
            var sessionJson = context.Session.GetString(SessionKey);
            var timestamp = context.Session.GetString(TimestampKey);

            if (sessionJson == null || timestamp == null)
                return null;

            var createdTime = DateTime.Parse(timestamp);
            if (DateTime.Now - createdTime > Expiration)
                return null; // Süresi dolmuş. null dönerse yeni bir session alınacak

            return JsonSerializer.Deserialize<SessionData>(sessionJson);
        }

        public static void Save(HttpContext context, SessionData session)
        {
            context.Session.SetString(SessionKey, JsonSerializer.Serialize(session));
            context.Session.SetString(TimestampKey, DateTime.Now.ToString("o")); // ISO 8601 format
        }
    }
}
