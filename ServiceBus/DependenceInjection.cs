using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceBus.Base;
using ServiceBus.Consumers;
using ServiceBus.Interfaces;
using System.Configuration;


namespace ServiceBus
{
    public static class DependenceInjection
    {

        public static IServiceCollection AddServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            // Registrar consumidores
            services.AddScoped<CriarProdutosConsumer>();
            services.AddScoped<AtualizarProdutosConsumer>();


            // Registrar a fábrica do consumidor
            services.AddSingleton<RabbitConsumerFactory>();

            // Registrar as interfaces dos consumidores
            services.AddTransient<ICriarProdutoConsumer, CriarProdutosConsumer>();
            services.AddTransient<IAtualizarProdutoConsumer, AtualizarProdutosConsumer>();



            return services;
        }
    }
}
