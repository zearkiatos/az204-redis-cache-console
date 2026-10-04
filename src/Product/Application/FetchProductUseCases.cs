using System.Diagnostics;
using RedisCacheConsole.Product.Application.dto;
using RedisCacheConsole.Product.Application.mapper;
using RedisCacheConsole.Product.Application.ports.output;
using System.Threading.Tasks;
using RedisCacheConsole.Product.Domain.Repository;
using RedisCacheConsole.Shared.Criterials.Domain;


namespace RedisCacheConsole.Product.Application
{

    class FetchProductUseCases : FetchProductOutputPort
    {
        private readonly ProductRepository productRepository;
        private readonly ProductCacheRepository productCacheRepository;
        public FetchProductUseCases(ProductRepository productRepository, ProductCacheRepository productCacheRepository)
        {
            this.productRepository = productRepository;
            this.productCacheRepository = productCacheRepository;
        }
        public async Task FetchTopProductsWithCache(
            string redisConnectionString,
            string cacheKey,
            string dataBaseConnectionString
        )
        {
            try
            {
                Console.WriteLine("Connecting to Azure Cache for Redis...");
                this.productCacheRepository.Setup(redisConnectionString);
                Console.WriteLine("Connection to Azure Cache for Redis established.\n");

                Console.WriteLine("Fetching data directly from Azure SQL Database...");
       
                this.productRepository.Setup(dataBaseConnectionString);
                Stopwatch stopwatch = Stopwatch.StartNew();
                var criteria = new Criterial(new List<Filter>(), "", 0, 10);
                var dataFromSql = ProductMapper.MapToJsonStringify(this.productRepository.Fetch(criteria));
                stopwatch.Stop();
                Console.WriteLine($"SQL query latency: {stopwatch.ElapsedMilliseconds} ms\n");

                Console.WriteLine("Storing data in Azure Cache for Redis...");
  
                await this.productCacheRepository.Save(cacheKey, dataFromSql, TimeSpan.FromMinutes(10));
                Console.WriteLine("Data stored in Redis.\n");

                Console.WriteLine("Fetching data from Azure Cache for Redis...");

                stopwatch.Restart();

                var dataFromRedis = await this.productCacheRepository.Get(cacheKey);
                stopwatch.Stop();
                Console.WriteLine($"Redis query latency: {stopwatch.ElapsedMilliseconds} ms\n");

                Console.WriteLine($"Data retrieved from Redis:\n{dataFromRedis}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}