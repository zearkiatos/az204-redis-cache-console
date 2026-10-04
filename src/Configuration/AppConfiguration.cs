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
            var envFileName = ".env";
            if (environment == "test")
                envFileName = ".env.test";
            else if (environment == "local") {
                envFileName = ".env.local";
            }
            var envFile = ResolveEnvFilePath(envFileName);
            Console.WriteLine($"Loading environment file: {envFile}");
            var options = new DotEnvOptions(envFilePaths: new[] { envFile });
            LoadEnv(options);
            return ReadEnv(options);
        }

        private static string ResolveEnvFilePath(string fileName)
        {
            foreach (var startDir in new[] { AppContext.BaseDirectory, Directory.GetCurrentDirectory() })
            {
                var dir = new DirectoryInfo(startDir);
                for (var i = 0; dir is not null && i < 6; i++, dir = dir.Parent)
                {
                    var candidate = Path.Combine(dir.FullName, fileName);
                    if (File.Exists(candidate))
                        return candidate;
                }
            }
            return fileName;
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