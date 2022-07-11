using RabbitMQBus.Event;

namespace RabbitMQBus.Interfaces
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
        Task Subscribe();
    }
}
