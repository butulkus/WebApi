using RabbitMQBus.Event;

namespace Catalog.Api.IntegrationEvents
{
    public interface ICatalogIntegrationEventService
    {
        void Publish(IntegrationEvent evt);
    }
}
