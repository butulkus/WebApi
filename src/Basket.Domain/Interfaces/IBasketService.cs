using Basket.Domain.Entities.BasketItemNS;

namespace Basket.Domain.Interfaces
{
    public interface IBasketService
    {
        Task<BasketItem> UpdateItemPrice(Guid id, decimal newPrice);
    }
}
