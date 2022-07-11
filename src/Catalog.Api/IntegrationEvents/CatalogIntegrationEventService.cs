using RabbitMQBus.Event;
using RabbitMQBus.Interfaces;

namespace Catalog.Api.IntegrationEvents
{
    public class CatalogIntegrationEventService : ICatalogIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        public CatalogIntegrationEventService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public void Publish(IntegrationEvent _event)
        {
            _eventBus.Publish(_event);
        }
    }
}
