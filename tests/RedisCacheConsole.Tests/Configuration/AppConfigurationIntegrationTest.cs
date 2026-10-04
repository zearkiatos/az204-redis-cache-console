using Configuration;

// AppConfiguration es estático y compartido por todo el assembly: sin esto, tests
// en otras collections pueden leer el mock (o el cache reseteado) de AppConfigurationUnitTest.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace RedisCacheConsole.Tests.Configuration
{
    // Sin mocks: lee la configuración real desde .env.test via ENVIRONMENT=test
    [Collection("AppConfiguration")]
    public class AppConfigurationIntegrationTest : IDisposable
    {
        public AppConfigurationIntegrationTest() => AppConfiguration.ResetForTests();

        [Fact]
        public void Given_Real_Environment_When_Get_Environment_Then_it_Should_Be_Test()
        {
            Assert.Equal("test", AppConfiguration.environment);
        }

        [Fact]
        public void Given_Real_Environment_When_Get_SQL_Connection_String_Then_it_Should_Not_Be_Empty()
        {
            Assert.False(string.IsNullOrWhiteSpace(AppConfiguration.sqlConnectionString));
        }

        [Fact]
        public void Given_Real_Environment_When_Get_Redis_Connection_String_Then_it_Should_Not_Be_Empty()
        {
            Assert.False(string.IsNullOrWhiteSpace(AppConfiguration.redisConnectionString));
        }

        [Fact]
        public void Given_Real_Environment_When_Get_Redis_Cache_Key_Then_it_Should_Not_Be_Empty()
        {
            Assert.False(string.IsNullOrWhiteSpace(AppConfiguration.redisCacheKey));
        }

        [Fact]
        public void Given_Real_Environment_When_ValidateConfiguration_Then_it_Should_Not_Throw()
        {
            var exception = Record.Exception(() => AppConfiguration.ValidateConfiguration());
            Assert.Null(exception);
        }

        public void Dispose() => AppConfiguration.ResetForTests();
    }
}
