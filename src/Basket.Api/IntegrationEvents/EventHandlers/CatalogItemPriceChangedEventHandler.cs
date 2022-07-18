using Basket.Api.IntegrationEvents.Events;
using Basket.Domain.Interfaces;
using RabbitMQBus.Event;
using RabbitMQBus.Interfaces;

namespace Basket.Api.IntegrationEvents.EventHandlers
{
    public class CatalogItemPriceChangedEventHandler : IEventHandler<CatalogItemPriceChangedEvent>
    {
        private readonly ILogger<CatalogItemPriceChangedEventHandler> _logger;
        private readonly IBasketService _basketService;

        public CatalogItemPriceChangedEventHandler(
            ILogger<CatalogItemPriceChangedEventHandler> logger,
            IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }

        public Task Handle(CatalogItemPriceChangedEvent @event)
        {
            _logger.LogTrace("Handling event: {event}", @event.GetType().Name);

            return _basketService.UpdateItemPrice(@event.ItemId, @event.NewPrice);
        }
    }
}
