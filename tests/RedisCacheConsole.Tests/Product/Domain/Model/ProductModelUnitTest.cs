using Bogus;
using RedisCacheConsole.Product.Domain.Model;
using Xunit;

namespace RedisCacheConsole.Tests.Product.Domain.Model
{
    public class ProductModelUnitTest
    {
        private static readonly Faker faker = new();
        [Fact]
        public void Given_a_ProductModel_Constructor_When_InitializedWithNullFilters_Then_It_ShouldCreate_it_withANameAndPriceList()
        {
            var name = faker.Commerce.ProductName();
            var priceList = faker.Random.Decimal(1000, 5000);
            var productModel = new ProductModel(name: name, listPrice: priceList);

            
            Assert.Equal(name, productModel.Name);
            Assert.Equal(priceList, productModel.ListPrice);
        }
        
    }
}