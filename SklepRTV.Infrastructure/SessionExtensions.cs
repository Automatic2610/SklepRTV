using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Text;

namespace SklepRTV.Infrastructure
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonConvert.SerializeObject(value)); 
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.Get<string>(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
