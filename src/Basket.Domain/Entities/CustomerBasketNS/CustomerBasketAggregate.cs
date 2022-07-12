using Basket.Domain.Entities.BasketItemNS;
using Basket.Domain.Exceptions;
using Basket.Domain.Interfaces;

namespace Basket.Domain.Entities.CustomerBasketNS
{
    public partial class CustomerBasket : IAggregateRoot
    {
        public CustomerBasket(Guid customerId, List<BasketItem> items)
        {
            CustomerId = customerId;
            Items = items;
        }

        public Guid GetCustomerId => CustomerId;
        public List<BasketItem> GetItems => Items;

        public void AddBasketItem(Guid id, string productName, decimal price, decimal oldPrice, string itemHashCode)
        {
            var priceLessOrEqualZero = price <= 0 || oldPrice <= 0;
            if (priceLessOrEqualZero)
                throw new DomainPriceException("invalid price");

            var newItem = new BasketItem(id, productName, price, oldPrice, itemHashCode);
            Items.Add(newItem);
        }
    }
}
