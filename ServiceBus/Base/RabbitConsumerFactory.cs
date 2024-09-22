using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using ServiceBus.Consumers;
using ServiceBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Base
{
    public class RabbitConsumerFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<RabbitConsumer> _consumers = new List<RabbitConsumer>();

        public RabbitConsumerFactory(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public void InitializeConsumers()
        {
            var rabbitConfig = _configuration.GetSection("RabbitConsumers").GetChildren();

            foreach (var consumerConfig in rabbitConfig)
            {
                var queueName = consumerConfig.GetSection("QueueName").Value;
                var exchange = consumerConfig.GetSection("ExchangeName").Value;
                var routingKey = consumerConfig.GetSection("RoutingKey").Value;

                var host = _configuration.GetSection("RabbitConnection:HostName").Value;
                var user = _configuration.GetSection("RabbitConnection:UserName").Value;
                var password = _configuration.GetSection("RabbitConnection:Password").Value;

                // Obter o tipo de consumidor a partir das configurações
                var consumerTypeName = consumerConfig.GetSection("ConsumerType");
                var consumerType = Type.GetType(consumerTypeName.Value);

                if (consumerType == null)
                {
                    throw new Exception($"Não foi possível encontrar o tipo de consumidor: {consumerTypeName}");
                }

                // Resolve o consumidor a partir do IServiceProvider usando um escopo
                using (var scope = _serviceProvider.CreateScope())
                {
                    var consumerInstance = scope.ServiceProvider.GetService(consumerType);

                    if (consumerInstance == null)
                    {
                        throw new Exception($"Não foi possível resolver a instância para o consumidor: {consumerTypeName}");
                    }

                    var consumer = new RabbitConsumer(host, user, password, queueName, exchange, routingKey, consumerInstance);
                    _consumers.Add(consumer);
                    consumer.StartConsuming();
                }
            }
        }
    }
}