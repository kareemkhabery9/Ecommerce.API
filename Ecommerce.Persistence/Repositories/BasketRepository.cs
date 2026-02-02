using Ecommerce.Domain.BasketModule;
using Ecommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Persistence.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository( IConnectionMultiplexer connection )
        {
            _database = connection.GetDatabase();
        }
        public async Task<CustomerBasket?> CreateOrUpdateAsync(CustomerBasket basket, TimeSpan timeToLive)
        {
            // Serialize the basket object to a JSON string for set it in Redis
            var jsonBasket = JsonSerializer.Serialize( basket );
            // Store the JSON string in Redis with the basket ID as the key and set an expiration time
            var isCreatedOrUpdated =  await _database.StringSetAsync(basket.Id, jsonBasket,
               (timeToLive == default) ? TimeSpan.FromDays(7) : timeToLive);

            if (isCreatedOrUpdated)
            {
                // Retrieve the basket back from Redis to ensure it was stored correctly
                var BasketReturned = await _database.StringGetAsync(basket.Id);

                // Deserialize the JSON string back to a CustomerBasket object before returning
                return JsonSerializer.Deserialize<CustomerBasket>(BasketReturned!);
            }

            else
                return null;

        }


        public Task<bool> DaletaBasketAsync(string basketId) => _database.KeyDeleteAsync(basketId);


        public async Task<CustomerBasket?> GetCustomerAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            if (basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(basket!);

        }

    }
}
