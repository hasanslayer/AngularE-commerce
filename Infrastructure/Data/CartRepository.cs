using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.Data
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;
        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteCardAsync(string cartId)
        {
            return await _database.KeyDeleteAsync(cartId);
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
           var data = await _database.StringGetAsync(cartId);

           return data.IsNullOrEmpty ? null : JsonConvert.DeserializeObject<Cart>(data);
        }

        public async Task<Cart> UpdateCartAsync(Cart cart)
        {
            // create or update(replace with cart that we get from Redis)
            //* hang in memory for 30 days
            var created = await _database.StringSetAsync(cart.Id,JsonConvert.SerializeObject(cart),TimeSpan.FromDays(30));

            if(!created){
                return null;
            }

            return await GetCartAsync(cart.Id);


        }
    }
}