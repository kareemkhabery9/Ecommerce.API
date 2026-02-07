using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Services.Abstraction
{
    public interface ICacheService
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, object Cachevalue, TimeSpan timeToLive);

    }
}
