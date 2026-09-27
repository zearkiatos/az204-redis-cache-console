using dotenv.net;

namespace Configuration
{
    public static class AppConfiguration
    {
        internal static Func<DotEnvOptions, IDictionary<string, string>> ReadEnv = DotEnv.Read;
        internal static Action<DotEnvOptions> LoadEnv = DotEnv.Load;

        private static IDictionary<string, string>? envVars;

        private static IDictionary<string, string> EnvVars => envVars ??= Initialize();

        private static IDictionary<string, string> Initialize()
        {
            var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
            var envFile = environment == "test" ? ".env.test" : ".env";
            var options = new DotEnvOptions(envFilePaths: new[] { envFile });
            LoadEnv(options);
            return ReadEnv(options);
        }

        // Solo para tests: fuerza a que la próxima lectura re-ejecute Initialize().
        internal static void ResetForTests() => envVars = null;

        public static string environment => 
            EnvVars["ENVIRONMENT"] 
            ?? throw new InvalidOperationException("ENVIRONMENT environment variable is not set");

        public static string sqlConnectionString => 
            EnvVars["SQL_CONNECTION_STRING"] 
            ?? throw new InvalidOperationException("SQL_CONNECTION_STRING environment variable is not set");

        public static string redisConnectionString =>
            EnvVars["REDIS_CONNECTION_STRING"] 
            ?? throw new InvalidOperationException("REDIS_CONNECTION_STRING environment variable is not set");

        public static string redisCacheKey =>
            EnvVars["REDIS_CACHE_KEY"] 
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