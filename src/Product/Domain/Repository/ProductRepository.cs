using System.Collections.Generic;
using RedisCacheConsole.Product.Domain.Model;
using RedisCacheConsole.Shared.Criterials.Domain;

namespace RedisCacheConsole.Product.Domain.Repository
{
    public interface ProductRepository
    {
        List<ProductModel> Fetch(Criterial criterial);
    }
}