using Ecommerce.Domain.Entities.ProductModule;
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

        //Specification to get the count of products based on filtering criteria
        public ProductsWithCountSpesifications(ProductQueryParam queryParams)
                : base(ProductSpecificationsHelper.GetCriteria(queryParams))
        {

        }


    }
}
