using System;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using StackExchange.Redis;
using Configuration;
using RedisCacheConsole.Product.Application;
using RedisCacheConsole.Product.Domain.Repository;
using RedisCacheConsole.Product.Infrastructure.Repository;

namespace RedisCacheConsole
{

    class Program
    {
        private static readonly string sqlConnectionString = AppConfiguration.sqlConnectionString;

        private static readonly string redisConnectionString = AppConfiguration.redisConnectionString;

        private static readonly string redisCacheKey = AppConfiguration.redisCacheKey;

        static async Task Main(string[] args)
        {
            var fetchProductUseCases = new FetchProductUseCases(
                new SqlServerProductRepository(),
                new RedisProductCacheRepository()
            );
            
            await fetchProductUseCases.FetchTopProductsWithCache(
                redisConnectionString,
                redisCacheKey,
                sqlConnectionString
            );
        }
    }
}
