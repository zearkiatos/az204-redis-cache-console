using System.Threading.Tasks;
namespace RedisCacheConsole.Product.Domain.Repository
{
    public interface ProductCacheRepository
    {
        Task Save(string key, string value);

        Task<string?> Get(string key);

        void Setup(string connectionString);
    }
}