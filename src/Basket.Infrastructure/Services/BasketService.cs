using AutoMapper;
using Basket.Domain.Entities.BasketItemNS;
using Basket.Domain.Interfaces;

namespace Basket.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper; 

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<BasketItem> UpdateItemPrice(Guid id, decimal newPrice)
        {
            var basketItem = await _basketRepository.GetBasketItemById(id);

            if (basketItem == null)
                throw new ArgumentNullException("Entity not found");

            basketItem.Update(
                basketItem.GetProductName(),
                basketItem.GetCurrentPrice(),
                basketItem.GetOldPrice(),
                basketItem.GetItemHashCode());

            var updatedItem = await _basketRepository.UpdateBasketItemPrice(basketItem);

            return updatedItem;
        }
    }
}
