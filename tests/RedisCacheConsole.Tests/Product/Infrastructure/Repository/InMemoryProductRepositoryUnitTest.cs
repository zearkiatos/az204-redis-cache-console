using Bogus;
using RedisCacheConsole.Product.Infrastructure.Repository;
using RedisCacheConsole.Product.Domain.Model;

namespace RedisCacheConsole.Tests.Product.Infrastructure.Repository
{
    public class InMemoryProductRepositoryUnitTest
    {
        private static readonly Faker faker = new();
        [Fact]
        public void Given_a_ProductModel_Constructor_When_InitializedWithNullFilters_Then_It_ShouldCreate_it_withANameAndPriceList()
        {
            var name = faker.Commerce.ProductName();
            var priceList = faker.Random.Decimal(1000, 5000);
            var productModel = new ProductModel(name: name, listPrice: priceList);
            var inMemoryProductRepository = new InMemoryProductRepository();
            inMemoryProductRepository.Save(productModel);

            var fetchedProducts = inMemoryProductRepository.Fetch(null);
            var fetchedProduct = fetchedProducts.FirstOrDefault();

            Assert.NotNull(fetchedProduct);
            Assert.Equal(name, fetchedProduct.Name);
            Assert.Equal(priceList, fetchedProduct.ListPrice);
            Assert.Equal(name, productModel.Name);
            Assert.Equal(priceList, productModel.ListPrice);
        }
    }
}