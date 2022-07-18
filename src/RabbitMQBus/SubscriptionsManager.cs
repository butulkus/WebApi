using RabbitMQBus.Event;
using RabbitMQBus.Interfaces;

namespace RabbitMQBus
{
    public class SubscriptionsManager : ISubscriptionsManager
    {
        private readonly Dictionary<string, List<Type>> _handlers;
        private readonly List<Type> _events;

        public SubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<Type>>();
            _events = new List<Type>();
        }

        public void SubscribeHandlerForEvent<EV, EH>()
            where EV : IntegrationEvent
            where EH : IEventHandler
        {
            var handlerType = typeof(EH);
            var eventType = typeof(EV);
            var eventName = eventType.Name;

            if (!_events.Contains(eventType))
            {
                _events.Add(eventType);
            }

            if (!IsAlreadySubscribe(eventName))
            {
                _handlers.Add(eventName, new List<Type>());
            }

            if (!_handlers[eventName].Any(x => x == handlerType))
            {
                _handlers[eventName].Add(handlerType);
            }
            else
            {
                throw new ArgumentException($"Handler: {handlerType.Name} for event: {eventName} already existed");
            }
            
        }

        public IEnumerable<Type> GetHandlersForEvent(string eventName)
        {
            var handlers = _handlers[eventName];

            return handlers;
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _events.SingleOrDefault(t => t.Name == eventName);
        }

        private bool IsAlreadySubscribe(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }
    }
}
