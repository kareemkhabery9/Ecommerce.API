using AutoMapper;
using Ecommerce.Domain.BasketModule;
using Ecommerce.Shared.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.MappingProfile
{
    internal class BasketProfile : Profile
    {
        public BasketProfile()
        {
          
            CreateMap<CustomerBasket, BasketDTO>().ReverseMap();

            CreateMap<BasketItems, BasketItemsDTO>().ReverseMap();
        }
    }
}
