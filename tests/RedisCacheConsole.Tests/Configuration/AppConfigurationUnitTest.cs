using Configuration;

namespace RedisCacheConsole.Tests.Configuration
{
    
    [Collection("AppConfiguration")]
    public class AppConfigurationUnitTest : IDisposable
    {
        public AppConfigurationUnitTest()
        {
            AppConfiguration.ResetForTests();
            AppConfiguration.LoadEnv = _ => { }; // no toca el entorno real ni el disco
            AppConfiguration.ReadEnv = _ => new Dictionary<string, string>
            {
                ["ENVIRONMENT"] = "test",
                ["SQL_CONNECTION_STRING"] = "Server=fake;Database=fake;",
                ["REDIS_CONNECTION_STRING"] = "fake-redis:6380",
                ["REDIS_CACHE_KEY"] = "fake-key"
            };
        }

        [Fact]
        public void Given_An_Environment_When_Get_Environment_Then_it_Should_Be_Equal_To_Test()
        {
            var environment = AppConfiguration.environment;
            Assert.Equal("test", environment);
        }

        [Fact]
        public void Given_A_SQL_Connection_String_When_Get_SQL_Connection_String_Then_it_Should_Be_Equal_To_Fake()
        {
            var sqlConnectionString = AppConfiguration.sqlConnectionString;
            Assert.Equal("Server=fake;Database=fake;", sqlConnectionString);
        }

        [Fact]
        public void Given_A_Redis_Connection_String_When_Get_Redis_Connection_String_Then_it_Should_Be_Equal_To_Fake()
        {
            var redisConnectionString = AppConfiguration.redisConnectionString;
            Assert.Equal("fake-redis:6380", redisConnectionString);
        }

        [Fact]
        public void Given_A_Redis_Cache_Key_When_Get_Redis_Cache_Key_Then_it_Should_Be_Equal_To_Fake()
        {
            var redisCacheKey = AppConfiguration.redisCacheKey;
            Assert.Equal("fake-key", redisCacheKey);
        }

        [Fact]
        public void Given_Configuration_When_ValidateConfiguration_Then_it_Should_Not_Throw()
        {
            var exception = Record.Exception(() => AppConfiguration.ValidateConfiguration());
            Assert.Null(exception);
        }
    
        public void Dispose()
        {
            AppConfiguration.LoadEnv = dotenv.net.DotEnv.Load;
            AppConfiguration.ReadEnv = dotenv.net.DotEnv.Read;
            AppConfiguration.ResetForTests();
        }
    }
}