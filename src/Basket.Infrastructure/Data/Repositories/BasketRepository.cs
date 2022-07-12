using AutoMapper;
using Basket.Domain.Entities.BasketItemNS;
using Basket.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Basket.Infrastructure.Data.Repositories
{
    public class BasketRepository: IBasketRepository
    {
        private readonly ILogger<BasketRepository> _logger;
        private readonly IMapper _mapper;
        private readonly BasketDBContext _basketDBContext;

        public BasketRepository(
            ILogger<BasketRepository> logger,
            IMapper mapper,
            BasketDBContext basketDBContext)
        {
            _logger = logger;
            _mapper = mapper;
            _basketDBContext = basketDBContext;
        }

        public Task<BasketItem?> GetBasketItemById(Guid id)
        {
            return _basketDBContext.basketItems.AsNoTracking().FirstOrDefaultAsync(x => id.Equals(x.Id));
        }

        public async Task<BasketItem> UpdateBasketItemPrice(BasketItem item)
        {
            var entity = _basketDBContext.basketItems
            .Update(item)
            .Entity;

            await _basketDBContext.SaveChangesAsync();

            return entity;
        }
    }
}
