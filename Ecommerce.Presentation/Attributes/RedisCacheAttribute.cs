using Ecommerce.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Presentation.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMins;

        public RedisCacheAttribute(int durationInMins = 5)
        {
            _durationInMins=durationInMins;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 1 - Get CacheService from DI Container
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cacheKey = CreateKey(context.HttpContext.Request);
            // 2 - Check if data exist in cache
            var cacheValue = await cacheService.GetAsync(cacheKey);

            //if exist, return it from cache and skip exucting endpoint
            if(cacheValue is not null )
            {
                context.Result = new ContentResult
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                return;
            }

            //if ont exist, excute endpoint and store resualt in cache if response was 200OK
            var ExcutedContext = await next.Invoke();

            if(ExcutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey, result.Value!, TimeSpan.FromMinutes(_durationInMins));
            }

        }



        //Create CacheKey based on RequestPath & QuertParams
        private string CreateKey(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();

            key.Append(request.Path);
       
            foreach(var item in request.Query.OrderBy(x => x.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }

            return key.ToString();
        }
    }

}
