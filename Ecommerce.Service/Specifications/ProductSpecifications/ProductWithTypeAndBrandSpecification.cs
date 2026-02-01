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

        //Specification to get products with their types and brands based on filtering, sorting, and pagination criteria
        public ProductWithTypeAndBrandSpecification(ProductQueryParam queryParam) : 
            base(ProductSpecificationsHelper.GetCriteria(queryParam)) 

        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);


            //Sorting
            switch (queryParam.sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;

                default:
                    AddOrderBy(p => p.Id);
                    break;
            }


            ApplyPagination(queryParam.PageSize, queryParam.PageIndex );

        }



        public ProductWithTypeAndBrandSpecification(int id) : base(p => p.Id ==  id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
