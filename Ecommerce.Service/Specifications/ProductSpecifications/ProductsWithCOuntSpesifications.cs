using Ecommerce.Domain.ProductModule;
using Ecommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications.ProductSpecifications
{
    internal class ProductsWithCountSpesifications : BaseSpesification<Product, int>
    {

        public ProductsWithCountSpesifications(ProductQueryParam queryParams)
                : base(p => (!queryParams.brandId.HasValue || p.ProductBrandId == queryParams.brandId.Value) &&
                    (!queryParams.typeId.HasValue || p.ProductTypeId == queryParams.typeId.Value) &&
                    (string.IsNullOrEmpty(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower()))
            )
        {

        }


    }
}
