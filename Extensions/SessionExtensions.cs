using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auer.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, T value)
        {
            session.SetString(typeof(T).FullName, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session)
        {
            var value = session.GetString(typeof(T).FullName);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
