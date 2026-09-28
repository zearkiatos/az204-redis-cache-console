using RedisCacheConsole.Product.Infrastructure.Repository;

namespace RedisCacheConsole.Tests.Product.Infrastructure.Repository
{
    public class InMemoryCacheProductUnitTest
    {
        [Fact]
        public void Given_a_key_and_value_When_saved_Then_it_should_be_retrieved()
        {
            var repository = new InMemoryCacheProductRepository();
            var key = "testKey";
            var value = "testValue";

            repository.save(key, value);
            var fetchedValue = repository.get(key);

            Assert.Equal(value, fetchedValue);
        }
    }
}