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
    public class RabbitConfig

    {

        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<QueueConfig> Queues { get; set; }
    }

    public class QueueConfig
    {
        public string QueueName { get; set; }
        public string Exchange { get; set; }
        public string RoutingKey { get; set; }
        public string ConsumerType { get; set; }
    }
}

