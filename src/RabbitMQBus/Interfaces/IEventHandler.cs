using RabbitMQBus.Event;

namespace RabbitMQBus.Interfaces
{
    public interface IEventHandler<in TIntegrationEvent> : IEventHandler
    where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IEventHandler
    {
    }
}
