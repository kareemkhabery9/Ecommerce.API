using AutoMapper;
using Ecommerce.Domain.Entities.IdentityModule;
using Ecommerce.Shared.IdentityDTOs.Ecommerce.Shared.IdentityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.MappingProfile
{
    internal class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
