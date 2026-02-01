using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.ProductModule;
using Ecommerce.Services.Abstraction;
using Ecommerce.Services.Specifications.ProductSpecifications;
using Ecommerce.Shared;
using Ecommerce.Shared.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork=unitOfWork;
            _mapper=mapper;
        }

        public async Task<IEnumerable<BrandDTO>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

         // return _mapper.Map<IEnumerable<ProductBrand, BrandDTO>>(Brands);
            return _mapper.Map<IEnumerable<BrandDTO>>(Brands);
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProductsAsync(ProductQueryParam queryParams)
        {

           var repo = _unitOfWork.GetRepository<Product, int>();

            var spec = new ProductWithTypeAndBrandSpecification(queryParams);
            var Products = await repo.GetAllAsync(spec);

            var ProductWithCountSpec = new ProductsWithCountSpesifications(queryParams);
            var TotalCount = await repo.CountAsync(ProductWithCountSpec);

            var DataToReturn = _mapper.Map<IEnumerable<ProductDTO>>(Products);
            var CountOfReturnedData = DataToReturn.Count();

            return new PaginatedResult<ProductDTO> 
                (
                    queryParams.PageIndex,
                    CountOfReturnedData,
                     TotalCount,
                    DataToReturn
                );

        }

        public async Task<IEnumerable<TypeDTO>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            return _mapper.Map<IEnumerable<TypeDTO>>(Types);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await  _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);

            return _mapper.Map<ProductDTO>(product);
        }
    }
}
