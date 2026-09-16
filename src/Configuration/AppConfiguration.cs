using dotenv.net;

namespace Configuration
{
    public static class AppConfiguration
    {
        private static IDictionary<string, string> envVars = null;
        static AppConfiguration()
        {
            DotEnv.Load();
            envVars = DotEnv.Read();
        }

        public static string environment => 
            envVars["ENVIRONMENT"] 
            ?? throw new InvalidOperationException("ENVIRONMENT environment variable is not set");

        public static string sqlConnectionString => 
            envVars["SQL_CONNECTION_STRING"] 
            ?? throw new InvalidOperationException("SQL_CONNECTION_STRING environment variable is not set");

        public static string redisConnectionString =>
            envVars["REDIS_CONNECTION_STRING"] 
            ?? throw new InvalidOperationException("REDIS_CONNECTION_STRING environment variable is not set");

        public static string redisCacheKey =>
            envVars["REDIS_CACHE_KEY"] 
            ?? throw new InvalidOperationException("REDIS_CACHE_KEY environment variable is not set");

        public static void ValidateConfiguration()
        {
            try
            {
                _ = environment;
                _ = sqlConnectionString;
                _ = redisConnectionString; 
                _ = redisCacheKey; 
                Console.WriteLine("✓ Configuration validation passed");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"✗ Configuration validation failed: {ex.Message}");
                throw;
            }
        }
    }
}