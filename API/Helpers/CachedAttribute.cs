using System.Linq;
using System.Text;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToliveSeconds;
        public CachedAttribute(int timeToliveSeconds)
        {
            _timeToliveSeconds = timeToliveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // if the data that we request it not found in the cache of redis, then the data 
            // will be cached in memory of redis, but if not, then the request will be run to the database then 
            // when the data is received, it will be cached in the memory for the next same request. 

            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            // generate key
            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult()
                {
                    // create our own response to send directly from here
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };

                context.Result = contentResult;

                return;
            }

            var executedContext = await next(); // move to controller

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                // here after get the response from the database, then the data will be cached
                await cacheService.CacheReponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToliveSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            // ex. [FromQuery]ProductSpecParams

            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}