using RedisCacheConsole.Product.Domain.Repository;
using RedisCacheConsole.Product.Domain.Model;

namespace RedisCacheConsole.Product.Infrastructure.Repository
{
    public class InMemoryCacheProductRepository : ProductCacheRepository
    {
        private readonly HashSet<(string Key, string Value)> products = new HashSet<(string Key, string Value)>();

        public string? get(string key)
        {
            var product = products.FirstOrDefault(p => p.Key == key);
            return product.Value;
        }

        public void save(string key, string value)
        {
            products.Add((Key: key, Value: value));
        }

        public void Setup(string connectionString)
        {
            // No setup needed for in-memory cache repository
        }
    }
}