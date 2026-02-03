using Ecommerce.Domain.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Contracts
{
    public interface IBasketRepository
    {

        Task<CustomerBasket?> GetCustomerAsync (string basketId);
        Task<CustomerBasket?> CreateOrUpdateAsync (CustomerBasket basket, TimeSpan timeToLive = default);   
        Task<bool> DaletaBasketAsync (string basketId);
    }
}
