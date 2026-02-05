using Ecommerce.Domain.Contracts;
using Ecommerce.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepository;

        public CacheService(ICacheRepository cacheRepository)
        {
            _cacheRepository=cacheRepository;
        }

        public Task<string?> GetAsync(string key)
        {
            return _cacheRepository.GetAsync(key);
        }

        public async Task SetAsync(string key, object cacheValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            await _cacheRepository.SetAsync(key, value, timeToLive);
           
        }
    }
}
