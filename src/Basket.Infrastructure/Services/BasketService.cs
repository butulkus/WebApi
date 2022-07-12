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

            var item = basketItem.GetProductName;
            var newItem = new BasketItem(
                id,
                basketItem.GetProductName,
                newPrice,
                basketItem.GetOldPrice,
                basketItem.GetItemHashCode);

            var updatedItem = await _basketRepository.UpdateBasketItemPrice(newItem);

            return updatedItem;
        }
    }
}
