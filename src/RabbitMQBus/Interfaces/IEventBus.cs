using RabbitMQBus.Event;

namespace RabbitMQBus.Interfaces
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
        void Subscribe<EV, EH>()
            where EV : IntegrationEvent
            where EH : IEventHandler;
    }
}
