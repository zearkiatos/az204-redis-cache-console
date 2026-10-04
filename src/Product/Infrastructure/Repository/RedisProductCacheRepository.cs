using StackExchange.Redis;
using RedisCacheConsole.Product.Domain.Repository;

namespace RedisCacheConsole.Product.Infrastructure.Repository
{
    public class RedisProductCacheRepository : ProductCacheRepository
    {
        private IDatabase cache;

        public async Task Save(string key, string value, TimeSpan expiration)
        {
            await this.cache.StringSetAsync(key, value, default);
        }

        public async Task<string?> Get(string key)
        {
            return await this.cache.StringGetAsync(key);
        }

        public void Setup(string connectionString)
        {
            var redis = ConnectionMultiplexer.Connect(connectionString);
            this.cache = redis.GetDatabase();
        }
    }
}