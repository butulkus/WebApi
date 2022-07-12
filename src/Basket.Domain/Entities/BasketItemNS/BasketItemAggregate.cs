using Basket.Domain.Base;
using Basket.Domain.Entities.CustomerBasketNS;
using Basket.Domain.Exceptions;
using Basket.Domain.Interfaces;

namespace Basket.Domain.Entities.BasketItemNS
{
    public partial class BasketItem : IAggregateRoot
    {
        public BasketItem(Guid id, string productName, decimal currentPrice, decimal oldPrice, string hashCode) : this()
        {
            if (currentPrice <= 0)
            {
                throw new DomainPriceException("invalid price");
            }

            Id = id;
            ProductName = productName;
            CurrentPrice = currentPrice;
            OldPrice = oldPrice;
            ItemHashCode = hashCode;
        }

        public string GetItemHashCode => ItemHashCode;
        public string GetProductName => ProductName;
        public decimal GetPrice => CurrentPrice;
        public decimal GetOldPrice => OldPrice;
        public IEnumerable<CustomerBasket> GetCustomerBaskets => customerBaskets.AsReadOnly();
    }
}
