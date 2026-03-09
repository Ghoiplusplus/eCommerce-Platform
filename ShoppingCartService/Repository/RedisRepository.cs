using StackExchange.Redis;

namespace ShoppingCart.Repository
{
    public class RedisRepository
    {
        private string instanceName;
        private IDatabaseAsync cache;
        private string GetKey(string key) => $"{instanceName}:{key}";

        public RedisRepository()
        {
            instanceName = "cart";

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:6379");
            cache = redis.GetDatabase();
        }

        public async Task<HashEntry[]> GetAllAsync(string userId)
        {
            var ProductId = await cache.HashGetAllAsync(GetKey(userId));
            return ProductId.Length == 0 ? null : ProductId;
        }

        public async Task AddOrUpdateAsync(string userId, int ProductId, int Amount = 1)
        {
            await cache.HashIncrementAsync(GetKey(userId), ProductId, Amount);
            await cache.KeyExpireAsync(GetKey(userId), TimeSpan.FromDays(30));
        }

        public async Task DeleteAsync(string userId, int ProductId)
        {
            await cache.HashDeleteAsync(GetKey(userId), ProductId);
        }
    }
}
