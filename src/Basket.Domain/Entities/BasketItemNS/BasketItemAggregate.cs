using Basket.Domain.Base;
using Basket.Domain.Entities.CustomerBasketNS;
using Basket.Domain.Exceptions;
using Basket.Domain.Interfaces;

namespace Basket.Domain.Entities.BasketItemNS
{
    public partial class BasketItem : IAggregateRoot
    {
        public BasketItem(string productName, decimal currentPrice, decimal oldPrice) : this()
        {
            if (currentPrice <= 0)
            {
                throw new DomainPriceException("invalid price");
            }

            ProductName = productName;
            CurrentPrice = currentPrice;
            OldPrice = oldPrice;
        }

        public decimal GetPrice() => CurrentPrice;
        public decimal GetOldPrice() => OldPrice;
        public IEnumerable<CustomerBasket> GetCustomerBaskets => customerBaskets.AsReadOnly();
    }
}
