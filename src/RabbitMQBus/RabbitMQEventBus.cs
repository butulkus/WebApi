using Autofac;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using RabbitMQBus.Event;
using RabbitMQBus.EventsAndEventHandlersList;
using RabbitMQBus.Interfaces;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace RabbitMQBus
{
    public class RabbitMQEventBus : IEventBus
    {
        const string EXCHANGE_NAME = "RabbitMQEventBus";
        const string AUTOFAC_SCOPE_NAME = "RabbitMQEventBus";
        const string QUEUE_NAME = "MyQueue2";

        private IConnection _connection;
        private IModel _channel;
        private readonly int _retryAmount;

        private readonly ILifetimeScope _autofac;
        private readonly ISubscriptionsManager _subscriptionsManager;
        private readonly ILogger<RabbitMQEventBus> _logger;

        public RabbitMQEventBus(ILifetimeScope autofac, ISubscriptionsManager subscriptionsManager, ILogger<RabbitMQEventBus> logger, int retryAmount = 3)
        {
            _autofac = autofac;
            _subscriptionsManager = subscriptionsManager;
            _logger = logger;
            _retryAmount = retryAmount;

            _connection = CreateConnection("localhost");
            _channel = CreateChannel();
        }

        public void Publish(IntegrationEvent @event)
        {
            var polly = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(_retryAmount,
                              retryTime => TimeSpan.FromSeconds(Math.Pow(2, retryTime)),
                              (exception, time) =>
                              {
                                  _logger.LogError(exception,
                                                   "Exception appeared while trying to resend message, Time passed: {time}, ex: {exception}",
                                                   time.TotalSeconds,
                                                   exception.Message);
                              });

            var json = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(json);

            var routingKey = @event.GetType().Name;


            // Made with polly coz connection may
            // return exception
            polly.Execute(() =>
            {
                _logger.LogTrace("Publishing message...");

                _channel.BasicPublish(exchange: EXCHANGE_NAME,
                           routingKey: routingKey,
                           mandatory: true,
                           basicProperties: null,
                           body: body);
            });
        }

        public void Subscribe<EV, EH>()
            where EV : IntegrationEvent
            where EH : IEventHandler
        {
            var eventName = typeof(EV).Name;
            _subscriptionsManager.SubscribeHandlerForEvent<EV, EH>();

            _logger.LogTrace("Event: {event} was subscribed with handler: {handler}", eventName, typeof(EH).Name);

            BindQueue(eventName);

            ListenQueue(); 
        }

        private IConnection CreateConnection(string hostName)
        {
            _logger.LogTrace("Creating a connection");

            var factory = new ConnectionFactory { HostName = hostName, DispatchConsumersAsync = true };
            var connection = factory.CreateConnection();

            _logger.LogTrace("Connection was created, HostName: {hostname}", hostName);

            return connection;
        }

        private IModel CreateChannel()
        {
            _logger.LogTrace("Creating a channel");

            var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: EXCHANGE_NAME,
                                    type: "direct");

            channel.QueueDeclare(
                queue: QUEUE_NAME,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return channel;
        }

        private void BindQueue(string eventName)
        {
            _channel.QueueBind(queue: QUEUE_NAME,
                               exchange: EXCHANGE_NAME,
                               routingKey: eventName);

            _logger.LogTrace("Queue: {queuename} was binded", QUEUE_NAME);
        }

        private void ListenQueue()
        {
            _logger.LogTrace("Starting listen a queue");

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += Consumer_Received;

            _channel.BasicConsume(
                queue: QUEUE_NAME,
                autoAck: false,
                consumer: consumer);
        }

        private async Task Consumer_Received(object ch, BasicDeliverEventArgs eventArgs)
        {
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var eventName = eventArgs.RoutingKey;

            try
            {
                await HandleEvent(message, eventName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "----- Message was not handled {message}, event: {event}", message, eventName);
            }

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        }

        private async Task HandleEvent(string message, string eventName)
        {
            var subs = _subscriptionsManager.GetHandlersForEvent(eventName);

            using var autofac = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME);
            foreach (var events in subs)
            {
                var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);

                var integrationEvent = System.Text.Json.JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                var handler = autofac.ResolveOptional(events);

                // Have made await + Task because using
                // disposed it earlier than method had completed
                await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
            }
        }

        public void Dispose()
        {
            if (_channel != null)
            {
                _channel.Dispose();
            }

            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
