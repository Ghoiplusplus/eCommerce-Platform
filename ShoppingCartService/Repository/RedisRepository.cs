using Microsoft.Extensions.Caching.Distributed;
using ShoppingCart.Model;
using StackExchange.Redis;
using System.Text.Json;

namespace ShoppingCart.Repository
{
    public class RedisRepository(IDatabaseAsync cache)
    {
        public async Task<RedisValue> GetAsync(string key)
        {
            var ProductId = await cache.StringGetAsync(key);
            if (ProductId == RedisValue.Null)
            {
                return RedisValue.Null;
            }
            return ProductId;
        }

        public async Task AddAsync(string key, int ProductId)
        {
            await cache.SetAddAsync(key, ProductId);
        }

        public async Task UpdateQuantityAsync(string key, int ProductId)
        {
            await cache.HashIncrementAsync(key, ProductId, 1);
        }

        public async Task DeleteAsync(string key, int ProductId)
        {
            await cache.HashDeleteAsync(key, ProductId);
        }
    }
}
