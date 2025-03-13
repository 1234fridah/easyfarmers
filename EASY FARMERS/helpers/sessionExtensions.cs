using Microsoft.AspNetCore.Http;
using System.Text.Json; // ✅ Use built-in System.Text.Json

namespace easy_farmers.Helpers
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value)); // ✅ Serialize using System.Text.Json
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value); // ✅ Deserialize
        }
    }
}
