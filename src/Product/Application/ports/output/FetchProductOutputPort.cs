using System.Collections.Generic;
using Product.Application.dto;

namespace Product.Application.ports.output
{
    interface FetchProductOutputPort
    {
        List<ProductResponse> FetchTopProductsWithCache();
    }
}