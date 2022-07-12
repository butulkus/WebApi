using RabbitMQBus.Event;

namespace Basket.Api.IntegrationEvents.Events
{
    public class CatalogItemPriceChangedEvent : IntegrationEvent
    {
        public Guid ItemId { get; private init; }
        public decimal NewPrice { get; private init; }

        public CatalogItemPriceChangedEvent(Guid itemId, decimal newPrice)
        {
            NewPrice = newPrice;
            ItemId = itemId;
        }
    }
}
