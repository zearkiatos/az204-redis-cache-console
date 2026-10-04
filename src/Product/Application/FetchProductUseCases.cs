using Product.Application.ports.output;
using RedisCacheConsole.Product.Domain.Repository;

namespace Product.Application
{

    class FetchProductUseCases : FetchProductOutputPort
    {
        private readonly ProductRepository productRepository;
        private readonly ProductCacheRepository productCacheRepository;
        public FetchProductUseCases(ProductRepository productRepository, ProductCacheRepository productCacheRepository)
        {
            this.productRepository = productRepository;
            this.productCacheRepository = productCacheRepository;
        }
        public void FetchTopProductsWithCache()
        {
            throw new NotImplementedException();
        }
    }
}