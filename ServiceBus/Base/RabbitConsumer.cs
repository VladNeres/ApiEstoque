using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Base
{
    public class RabbitConsumer

    {
        private readonly string _queueName;
        private readonly string _exchange;
        private readonly string _routingKey;
        public readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly object _consumerInstance;

        public RabbitConsumer(string host, string user, string password, string queueName, string exchange, string routingKey, object consumerInstance)
        {
                _queueName = queueName;
                _exchange = exchange;
                _routingKey = routingKey;
                _consumerInstance = consumerInstance;

                var factory = new ConnectionFactory()
                {
                    HostName = host,
                    UserName = user,
                    Password = password
                };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Não foi possivel se conectar ao serviço de mensageria");
                return;

            }
        }

        public void StartConsuming()
        {
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: _exchange, routingKey: _routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Invoca o método ProcessMessage da instância consumidora
                var method = _consumerInstance.GetType().GetMethod("ProcessMessage");
                method?.Invoke(_consumerInstance, new object[] { message });

                _channel.BasicAck(ea.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }
    }
}
