using Ecommerce.Domain.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Specifications.ProductSpecifications
{
    internal class ProductWithTypeAndBrandSpecification : BaseSpesification<Product, int>
    {
        public ProductWithTypeAndBrandSpecification(int? brandId, int? typeId) : 
            base(p=>(!brandId.HasValue || p.ProductBrandId == brandId.Value) &&
                    (!typeId.HasValue || p.ProductTypeId == typeId.Value))
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
