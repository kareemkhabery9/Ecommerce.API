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
        public ProductWithTypeAndBrandSpecification() : base(null!)
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
