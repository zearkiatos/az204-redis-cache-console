using Configuration;
using RedisCacheConsole.Product.Infrastructure.Repository;
using RedisCacheConsole.Shared.Criterials.Domain;

namespace RedisCacheConsole.Tests.Product.Infrastructure.Repository
{
    [Collection("AppConfiguration")]
    public class SqlServerProductRepositoryIntegrationTest
    {
        [Fact]
        public void GivenAValidCriterial_WhenFetchingProducts_ThenReturnsProductList()
        {
            
            var repository = new SqlServerProductRepository();
            repository.Setup(AppConfiguration.sqlConnectionString);

            var criteria = new Criterial(new List<Filter>(), "", 0, 10);

            var products = repository.Fetch(criteria);

            Assert.NotNull(products);
            Assert.True(products.Count > 0);
            Assert.Equal(10, products.Count);
        }
    }
}