using RabbitMQBus.Event;

namespace RabbitMQBus.Interfaces
{
    public interface ISubscriptionsManager
    {
        void SubscribeHandlerForEvent<EV, HE>()
            where EV : IntegrationEvent
            where HE : IEventHandler;

        IEnumerable<Type> GetHandlersForEvent(string eventName);

        Type GetEventTypeByName(string eventName);
    }
}