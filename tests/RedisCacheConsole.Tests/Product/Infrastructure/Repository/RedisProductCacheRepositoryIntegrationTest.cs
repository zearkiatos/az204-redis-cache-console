using Configuration;
using Xunit;
using RedisCacheConsole.Product.Infrastructure.Repository;

namespace RedisCacheConsole.Tests.Product.Infrastructure.Repository
{
    [Collection("AppConfiguration")]
    public class RedisProductCacheRepositoryIntegrationTest
    {
        [Fact]
        public async Task GivenAValidKeyAndValue_WhenSavingAndFetching_ThenReturnsValue()
        {
            
            var repository = new RedisProductCacheRepository();
            repository.Setup(AppConfiguration.redisConnectionString);

            var key = $"app:test:{Guid.NewGuid():N}";
            var value = "test-value";

            await repository.Save(key, value);
            var fetchedValue = await repository.Get(key);

            Assert.NotNull(fetchedValue);
            Assert.Equal(value, fetchedValue);
        }   
    }
}
