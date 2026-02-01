using Ecommerce.Domain.ProductModule;
using Ecommerce.Shared;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications.ProductSpecifications
{
    //Helper class to avoid code duplication in specification classes
    internal static class ProductSpecificationsHelper
    {

        // Fun to get the criteria expression for filtering products based on query parameters and to use in spec classes 
        public static Expression<Func<Product, bool>> GetCriteria(ProductQueryParam queryParam)
        {
            return p => (!queryParam.brandId.HasValue || p.ProductBrandId == queryParam.brandId.Value) &&
                    (!queryParam.typeId.HasValue || p.ProductTypeId == queryParam.typeId.Value) &&
                    (string.IsNullOrEmpty(queryParam.search) || p.Name.ToLower().Contains(queryParam.search.ToLower()));

        }
    }
}
