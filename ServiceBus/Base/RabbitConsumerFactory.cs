using Microsoft.Extensions.Configuration;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using ServiceBus.Interfaces;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace ServiceBus.Base
{
    public class RabbitConsumerFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;

        public RabbitConsumerFactory(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;

            var factory = new ConnectionFactory
            {
                HostName = _configuration["RabbitConnection:HostName"],
                UserName = _configuration["RabbitConnection:UserName"],
                Password = _configuration["RabbitConnection:Password"]
            };

            _connection = factory.CreateConnection();
        }


        public async Task StartConsumers()
        {
            var queues = _configuration.GetSection("RabbitConsumers").Get<List<QueueConfig>>();

            foreach (var queueConfig in queues)
            {
                try
                {
                    var channel = _connection.CreateModel();

                    // Declaração da exchange e fila
                    channel.ExchangeDeclare(queueConfig.Exchange, ExchangeType.Direct, durable: true);
                    channel.QueueDeclare(queueConfig.QueueName, durable: true, exclusive: false, autoDelete: false);
                    channel.QueueBind(queueConfig.QueueName, queueConfig.Exchange, queueConfig.RoutingKey);

                    // Obter o tipo do consumidor e criar instância
                    var consumerType = Type.GetType(queueConfig.ConsumerType);
                    if (consumerType == null)
                    {
                        Console.WriteLine($"Consumer não encontrado: {queueConfig.ConsumerType}");
                        continue;
                    }

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var scopedProvider = scope.ServiceProvider;
                        var consumerInstance = (IConsumer)scopedProvider.GetService(consumerType);
                        if (consumerInstance == null)
                        {
                            Console.WriteLine($"Falha ao criar instância do consumer: {queueConfig.ConsumerType}");
                            continue;
                        }

                        // Configuração do EventingBasicConsumer
                        var eventingConsumer = new EventingBasicConsumer(channel);
                        eventingConsumer.Received += async (model, ea) =>
                        {
                            var body = ea.Body.ToArray();
                            var message = Encoding.UTF8.GetString(body);

                            try
                            {
                                Console.WriteLine($"Mensagem recebida: {message}");
                                await consumerInstance.ProcessMessageAsync(message);

                                // Confirmação manual da mensagem
                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                                channel.BasicNack(ea.DeliveryTag, false, true);
                            }
                        };


                        // Iniciar o consumo da fila
                        channel.BasicConsume(queueConfig.QueueName, autoAck: false, eventingConsumer);
                        Console.WriteLine($"Consumidor iniciado para a fila: {queueConfig.QueueName}");


                        // Verificar e consumir mensagens pendentes ao iniciar
                        var result = channel.BasicGet(queueConfig.QueueName, autoAck: false);
                        while (result != null)
                        {
                            var pendingMessage = Encoding.UTF8.GetString(result.Body.ToArray());
                            Console.WriteLine($"Consumindo mensagem pendente: {pendingMessage}");
                            await consumerInstance.ProcessMessageAsync(pendingMessage);
                            channel.BasicAck(result.DeliveryTag, false);
                            result = channel.BasicGet(queueConfig.QueueName, autoAck: false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao iniciar consumidor para a fila {queueConfig.QueueName}: {ex.Message}");
                }
            }
        }
    }
}