using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auer.Extensions
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, T value) where T : class
        {
            tempData[typeof(T).FullName] = JsonConvert.SerializeObject(value);
        }
        public static T Get<T>(this ITempDataDictionary tempData) where T : class
        {
            object o;
            tempData.TryGetValue(typeof(T).FullName, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
        public static T Peek<T>(this ITempDataDictionary tempData) where T : class
        {
            object o = tempData.Peek(typeof(T).FullName);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

    }
}
