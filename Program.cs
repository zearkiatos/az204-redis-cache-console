using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using Configuration;

class Program
{
    private static readonly string sqlConnectionString = AppConfiguration.sqlConnectionString;

    private static readonly string redisConnectionString = AppConfiguration.redisConnectionString;

    static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Connecting to Azure Cache for Redis...");
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            var cache = redis.GetDatabase();
            Console.WriteLine("Connection to Azure Cache for Redis established.\n");
            
            string cacheKey = AppConfiguration.redisCacheKey;

            Console.WriteLine("Fetching data directly from Azure SQL Database...");
            Stopwatch stopwatch = Stopwatch.StartNew();
            var dataFromSql = FetchFromSql();
            stopwatch.Stop();
            Console.WriteLine($"SQL query latency: {stopwatch.ElapsedMilliseconds} ms\n");

            Console.WriteLine("Storing data in Azure Cache for Redis...");
            
            await cache.StringSetAsync(cacheKey, dataFromSql, TimeSpan.FromMinutes(10));
            Console.WriteLine("Data stored in Redis.\n");

            Console.WriteLine("Fetching data from Azure Cache for Redis...");

            stopwatch.Restart();
            var dataFromRedis = await cache.StringGetAsync(cacheKey);
            stopwatch.Stop();
            Console.WriteLine($"Redis query latency: {stopwatch.ElapsedMilliseconds} ms\n");
            
            Console.WriteLine($"Data retrieved from Redis:\n{dataFromRedis}");
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
