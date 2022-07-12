using Basket.Domain.Base;
using Basket.Domain.Entities.CustomerBasketNS;

namespace Basket.Domain.Entities.BasketItemNS
{
    public partial class BasketItem : BaseEntity
    {
        protected BasketItem()
        {
            customerBaskets = new List<CustomerBasket>();
        }

        private string ProductName;
        private decimal CurrentPrice;
        private decimal OldPrice;
        private string ItemHashCode;

        private List<CustomerBasket> customerBaskets;
    }
}
