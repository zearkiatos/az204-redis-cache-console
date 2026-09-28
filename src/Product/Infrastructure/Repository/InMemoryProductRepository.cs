using RedisCacheConsole.Product.Domain.Repository;
using RedisCacheConsole.Product.Domain.Model;
using RedisCacheConsole.Shared.Criterials.Domain;

namespace RedisCacheConsole.Product.Infrastructure.Repository
{
    public class InMemoryProductRepository : ProductRepository
    {
        private readonly List<ProductModel> products = new List<ProductModel>();

        public void Save(ProductModel product)
        {
            products.Add(product);
        }

        public void Setup(string connectionString)
        {
            // No setup needed for in-memory repository
        }

        public List<ProductModel> Fetch(Criterial criterial)
        {
            return products;
        }
    }
}