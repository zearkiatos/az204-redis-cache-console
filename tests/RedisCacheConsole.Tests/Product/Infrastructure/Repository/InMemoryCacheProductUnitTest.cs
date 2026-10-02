using Xunit;
using System.Threading.Tasks;
using RedisCacheConsole.Product.Infrastructure.Repository;

namespace RedisCacheConsole.Tests.Product.Infrastructure.Repository
{
    public class InMemoryCacheProductUnitTest
    {
        [Fact]
        public async Task Given_a_key_and_value_When_saved_Then_it_should_be_retrieved()
        {
            var repository = new InMemoryCacheProductRepository();
            var key = "testKey";
            var value = "testValue";

            await repository.Save(key, value);
            var fetchedValue = await repository.Get(key);

            Assert.Equal(value, fetchedValue);
        }
    }
}