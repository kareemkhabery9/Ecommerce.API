using Ecommerce.Shared;
using Ecommerce.Shared.CommenResponses;
using Ecommerce.Shared.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Abstraction
{
    public interface IProductService
    {
       public Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParam queryParam);

       public Task<Result<ProductDTO>> GetProductByIdAsync(int id);

       public Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

       public Task<IEnumerable<TypeDTO>> GetAllTypesAsync();



    }
}
