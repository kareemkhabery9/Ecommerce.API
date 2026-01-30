using Ecommerce.Domain.ProductModule;
using Ecommerce.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications.ProductSpecifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpesification<Product, int>
    {
        public ProductWithTypeAndBrandSpecification(ProductQueryParam queryParam) : 
            base(p=>(!queryParam.brandId.HasValue || p.ProductBrandId == queryParam.brandId.Value) &&
                    (!queryParam.typeId.HasValue || p.ProductTypeId == queryParam.typeId.Value) &&
                    (string.IsNullOrEmpty(queryParam.search) || p.Name.ToLower().Contains(queryParam.search.ToLower()))
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }



        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id ==  id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
