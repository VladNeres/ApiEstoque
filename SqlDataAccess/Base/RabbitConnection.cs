using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBus.Base
{
    public class RabbitConnection
    {
        private readonly IConnection _connection;
        private readonly string _queueName;
        private readonly string _exchangeName;
        private readonly string _routingKey;

        public RabbitConnection(IConnection connection, string queueName)
        {
            _queueName = queueName;
            _connection = connection;
        }

        public async Task StartConsuming()
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
    }
}
