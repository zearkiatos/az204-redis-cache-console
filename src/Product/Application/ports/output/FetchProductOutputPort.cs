using System.Collections.Generic;
using RedisCacheConsole.Product.Application.dto;

namespace RedisCacheConsole.Product.Application.ports.output
{
    interface FetchProductOutputPort
    {
        Task FetchTopProductsWithCache(string cacheConnectionString, string cacheKey, string dataBaseConnectionString);
    }
}