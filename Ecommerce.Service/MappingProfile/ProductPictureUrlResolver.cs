using AutoMapper;
using AutoMapper.Execution;
using Ecommerce.Domain.ProductModule;
using Ecommerce.Shared.ProductDTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.MappingProfile
{
    internal class ProductPictureUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {

            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;

            //if the PictureUrl is already in another server, return it as is
            if (source.PictureUrl.StartsWith("http") || source.PictureUrl.StartsWith("https"))
                return source.PictureUrl;

            var baseUrl = _configuration.GetSection("URLs")["baseUrl"];
            var PictureUrl = $"{baseUrl}{source.PictureUrl}";

            return PictureUrl;

        }
    }
}
