using AutoMapper;
using Ecommerce.Domain.Entities.ProductModule;
using Ecommerce.Shared.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.MappingProfile
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {

            CreateMap<ProductBrand, BrandDTO>();


            CreateMap<ProductType, TypeDTO>();


            CreateMap<Product,  ProductDTO>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());
             
        }
    }

}
