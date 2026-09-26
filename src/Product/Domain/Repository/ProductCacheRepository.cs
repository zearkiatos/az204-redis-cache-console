
namespace RedisCacheConsole.Product.Domain.Repository
{
    public interface ProductCacheRepository
    {
        void save(string key, string value);

        string? get(string key);
    }
}