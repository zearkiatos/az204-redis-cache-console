using Product.Application.dto;
using Product.Domain.Model;
namespace Product.Application.mapper
{
    static public class ProductMapper
    {
        static ProductResponse MapToResponse(ProductModel product)
        {
            return new ProductResponse
            {
                Name = product.Name,
                PriceList = product.PriceList.ToString()
            };
        }
    }
}