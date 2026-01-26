using Ecommerce.Services.Abstraction;
using Ecommerce.Shared.ProductDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService=productService;
        }


        // Get all products
        [HttpGet]
        // GET:BaseUrl/api/products
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }


        // Get product by id
        [HttpGet("{id}")]
        // GET:BaseUrl/api/products/{id}
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            return Ok(product);
        }


        // Get all brands
        [HttpGet("brands")]
        // GET:BaseUrl/api/products/brands
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrandsAsync()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }


        // Get all types
        [HttpGet("types")]
        // GET:BaseUrl/api/products/types
        public async Task<ActionResult<IEnumerable<TypeDTO>>> GetAllTypesAsync()
        {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }


    }
}

