using Ecommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connection)
        {
          _database = connection.GetDatabase();   
        }

        public async Task<string?> GetAsync(string key)
        {
           return await _database.StringGetAsync(key);
        }

        public async Task SetAsync(string key, string value, TimeSpan timeToLive)
        {
            await _database.StringSetAsync(key, value, timeToLive);
        }
    }
}
