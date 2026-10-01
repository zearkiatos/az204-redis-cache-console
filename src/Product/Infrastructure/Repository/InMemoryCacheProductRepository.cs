using RedisCacheConsole.Product.Domain.Repository;
using RedisCacheConsole.Product.Domain.Model;

namespace RedisCacheConsole.Product.Infrastructure.Repository
{
    public class InMemoryCacheProductRepository : ProductCacheRepository
    {
        private readonly HashSet<(string Key, string Value)> products = new HashSet<(string Key, string Value)>();

        public Task<string?> Get(string key)
        {
            var product = products.FirstOrDefault(p => p.Key == key);
            return Task.FromResult(product.Value);
        }

        public Task Save(string key, string value)
        {
            this.products.Add((Key: key, Value: value));
            return Task.CompletedTask;
        }

        public void Setup(string connectionString)
        {
            // No setup needed for in-memory repository
        }
    }
}