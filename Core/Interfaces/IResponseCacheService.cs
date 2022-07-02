using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IResponseCacheService
    {
        // using cache in redis
        Task CacheReponseAsync(string cacheKey,object response,TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cacheKey);

    }
}