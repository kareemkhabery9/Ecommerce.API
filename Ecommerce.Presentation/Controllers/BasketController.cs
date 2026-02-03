using Ecommerce.Services.Abstraction;
using Ecommerce.Shared.BasketDTOs;
using Microsoft.AspNetCore.Http.HttpResults;
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
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService=basketService;
        }

        // this endpoint is used to get the basket by id and takes id as query parameter
        //Get:BaseUrl/api/Basket?id={id}
        [HttpGet]
        public async Task<ActionResult<BasketDTO>> GetBasket (string basketid)
        {
            var basket = await _basketService.GetBasketAsync(basketid);

            return Ok(basket);
        }


        //post:BaseUrl/api/Basket
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> CreateOrUpdate (BasketDTO basket)
        {
            var Basket = await _basketService.CreateOrUpdateBasketAsync(basket);

            return Ok(Basket);
        }


        // Delete basket by id and takes id from route
        //Delete:BaseUrl/api/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket (string id)
        {
            var result = await _basketService.DeleteBasketAsync(id);
            return Ok(result);
        }



    }
}
