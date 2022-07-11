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
        private IConnection _connection; // make readonly if works
        private IModel _channel;

        public RabbitMQEventBus()
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

        public async Task Subscribe() // it excites without await, mb replace with sync
        {
            BindToQueue();

            await ListenQueue(); 
        }

        private Task ListenQueue()
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (ch, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());

                // tut karoche method na obnovlenie vizivat
                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(
                "MyQueue",
                false,
                consumer);

            return Task.CompletedTask;
        }

        private void BindToQueue()
        {

        }
    }
}
