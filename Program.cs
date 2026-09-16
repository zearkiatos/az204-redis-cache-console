using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using Configuration;

class Program
{
    // Cadena de conexión a Azure SQL Database
    private static readonly string sqlConnectionString = AppConfiguration.sqlConnectionString;

    // Cadena de conexión principal a Azure Cache for Redis
    private static readonly string redisConnectionString = AppConfiguration.redisConnectionString;

    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Conectando a Azure Cache for Redis...");
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            var cache = redis.GetDatabase();
            Console.WriteLine("Conexión a Azure Cache for Redis establecida.\n");
            
            //Aquí creamos un nombre especial (clave) que usaremos para guardar y buscar datos en Redis.
            string cacheKey = "AdventureWorks_ProductList";

            // Prueba directa desde SQL
            Console.WriteLine("Recuperando datos directamente desde Azure SQL Database...");
            Stopwatch stopwatch = Stopwatch.StartNew();
            var dataFromSql = FetchFromSql();
            stopwatch.Stop();
            Console.WriteLine($"Latencia de consulta SQL: {stopwatch.ElapsedMilliseconds} ms\n");

            // Almacenar datos en Redis
            Console.WriteLine("Almacenando datos en Azure Cache for Redis...");
            
            //Guarda los datos obtenidos de SQL en Redis usando la clave definida. Estos datos estarán disponibles por 10 minutos.
            await cache.StringSetAsync(cacheKey, dataFromSql, TimeSpan.FromMinutes(10));
            Console.WriteLine("Datos almacenados en Redis.\n");

            // Recuperar datos desde Redis
            Console.WriteLine("Recuperando datos desde Azure Cache for Redis...");
            //Reinicia el cronómetro para medir el tiempo.
            stopwatch.Restart();
            //Recupera los datos desde Redis usando la clave que definimos antes.
            var dataFromRedis = await cache.StringGetAsync(cacheKey);
            stopwatch.Stop();
            Console.WriteLine($"Latencia de consulta Redis: {stopwatch.ElapsedMilliseconds} ms\n");

            // Mostrar datos recuperados desde Redis
            Console.WriteLine($"Datos recuperados desde Redis:\n{dataFromRedis}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static string FetchFromSql()
    {
        using (var connection = new SqlConnection(sqlConnectionString))
        {
            connection.Open();
            var command = new SqlCommand("SELECT TOP 10 [Name], [ListPrice] FROM [SalesLT].[Product] FOR JSON PATH", connection);
            var reader = command.ExecuteReader();
            reader.Read();
            return reader.GetString(0);
        }
    }
}
