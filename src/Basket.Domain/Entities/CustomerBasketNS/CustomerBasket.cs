using Basket.Domain.Base;
using Basket.Domain.Entities.BasketItemNS;

namespace Basket.Domain.Entities.CustomerBasketNS
{
    public partial class CustomerBasket : BaseEntity
    {
        protected CustomerBasket()
        {
            Items = new List<BasketItem>();
        }

        private Guid CustomerId;
        private List<BasketItem> Items;
    }
}
