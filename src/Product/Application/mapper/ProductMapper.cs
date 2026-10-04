using RedisCacheConsole.Product.Application.dto;
using RedisCacheConsole.Product.Domain.Model;
namespace RedisCacheConsole.Product.Application.mapper
{
    static public class ProductMapper
    {
        static public ProductResponse MapToResponse(ProductModel product)
        {
            return new ProductResponse
            {
                Name = product.Name,
                ListPrice = product.ListPrice.ToString()
            };
        }

        static public string MapToJsonStringify(List<ProductModel> product)
        {
            return "[" + string.Join(",", product.ConvertAll(p => p.ToJsonStringify())) + "]";
        }
    }
}