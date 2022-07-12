using Autofac;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQBus.Event;
using RabbitMQBus.Interfaces;
using System.Text;
using System.Text.Json;

namespace RabbitMQBus
{
    public class RabbitMQEventBus : IEventBus
    {
        const string AUTOFAC_SCOPE_NAME = "RabbitMQEventBus";

        private IConnection _connection; // make readonly if works
        private IModel _channel;
        private readonly ILifetimeScope _autofac;

        public RabbitMQEventBus(ILifetimeScope autofac)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: "MyQueue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _autofac = autofac;
        }

        public void Publish(IntegrationEvent @event)
        {
            var json = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                           routingKey: "MyQueue",
                           basicProperties: null,
                           body: body);
        }

        public void Subscribe()
        {
            BindToQueue();

            ListenQueue(); 
        }

        private void ListenQueue()
        {
            /* using */var autofac = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME); // be carefull with string param(name of scope)

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                var eventType = Type.GetType("Basket.Api.IntegrationEvents.Events.CatalogItemPriceChangedEvent,Basket.Api");
                var integrationEvent = System.Text.Json.JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                var handlerType = Type.GetType("Basket.Api.IntegrationEvents.EventHandlers.CatalogItemPriceChangedEventHandler,Basket.Api");
                var handler = autofac.ResolveOptional(handlerType);

                concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(
                "MyQueue",
                false,
                consumer);
        }

        private void BindToQueue()
        {

        }
    }
}
