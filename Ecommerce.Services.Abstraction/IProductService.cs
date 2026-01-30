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
       public Task<IEnumerable<ProductDTO>> GetAllProductsAsync(int? brandId, int? typeId);

       public Task<ProductDTO> GetProductByIdAsync(int id);

       public Task<IEnumerable<BrandDTO>> GetAllBrandsAsync();

       public Task<IEnumerable<TypeDTO>> GetAllTypesAsync();



    }
}
