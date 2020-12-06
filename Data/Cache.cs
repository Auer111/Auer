
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Auer.Data
{

    /// <summary>
    /// Allows data to be stored/ fetched from memory for a short period of time after an initial data fetch.
    /// </summary>
    /// <example>
    /// var products = cache.GetOrSet("_products",60,()=>productRepository.GetAll())
    /// </example>
    public class Cache
    {
        IMemoryCache _cache { get; set; }
        public Cache(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public object GetorSet<T>(string key, int duration, Func<T> getItemCallback ) 
        {
            T item;
            if (!_cache.TryGetValue(key, out item))
            {
                item = getItemCallback();

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(duration));
                _cache.Set(key, item, cacheEntryOptions);
            }
            return item;
        }

    }
}


