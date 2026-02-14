using AutoMapper;
using Ecommerce.Domain.Contracts;
using Ecommerce.Domain.Entities.BasketModule;
using Ecommerce.Services.Abstraction;
using Ecommerce.Services.Exceptions;
using Ecommerce.Shared.BasketDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository=basketRepository;
            _mapper=mapper;
        }

        //this fun takes BasketDTO as parameter because the service layer works with DTOs and not domain entities directly 
        //and returns BasketDTO after creating or updating the basket
        public async Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO CreateOrUpdateBasket)
        {
            // Map BasketDTO to CustomerBasket 
            var CustomerBasket = _mapper.Map<CustomerBasket>(CreateOrUpdateBasket);
            // Call Repository to Create or Update Basket that takes CustomerBasket as parameter and returns CustomerBasket
            var CteatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateAsync(CustomerBasket);
            // Map CustomerBasket back to BasketDTO
            return _mapper.Map<BasketDTO>(CteatedOrUpdatedBasket);
        }

        public async Task<bool> DeleteBasketAsync(string basketId) => await _basketRepository.DaletaBasketAsync(basketId);
      


        public async Task<BasketDTO> GetBasketAsync(string basketId)
        {
           var basket = await _basketRepository.GetCustomerAsync(basketId);

            if (basket == null)
                throw new BasketNotFoundException(basketId);

            return _mapper.Map<BasketDTO>(basket);


        }
    }
}
