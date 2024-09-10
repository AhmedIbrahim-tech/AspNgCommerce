using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastrucure.Repositories.Transactions
{
    public class MemoryBasketRepository : IBasketRepository
    {
        private readonly IMemoryCache _memoryCache;
        private const string CacheKeyPrefix = "Basket_";

        public MemoryBasketRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            if (_memoryCache.TryGetValue(CacheKeyPrefix + basketId, out CustomerBasket basket))
            {
                return basket;
            }
            await Task.CompletedTask;
            return null;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            _memoryCache.Set(CacheKeyPrefix + customerBasket.Id, customerBasket, TimeSpan.FromDays(30));
            await Task.CompletedTask;
            return customerBasket;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            _memoryCache.Remove(CacheKeyPrefix + basketId);
            await Task.CompletedTask;
            return true;
        }

    }
}
