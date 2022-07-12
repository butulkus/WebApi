using Basket.Domain.Entities.BasketItemNS;

namespace Basket.Domain.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketItem?> GetBasketItemById(Guid id);
        Task<BasketItem> UpdateBasketItemPrice(BasketItem item);
    }
}
