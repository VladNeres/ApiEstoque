using Aplication.Interface;
using Aplication.Service;
using ServiceBus.Base;
using ServiceBus.Consumers;
using ServiceBus.Interfaces;
using SqlDataAccess.Repositories;
using SqlDataAccess.Repositories.Interface;

namespace ApiEstoque.DependenceInjecton
{
    public static class DependenceInjection
    {

        public static IServiceCollection InjectionDependence(IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddScoped<IEstoqueService, EstoqueService>();
            
            //Repository
            services.AddScoped<IEstoqueRepository, EstoqueRepository>(e => new EstoqueRepository(configuration["ConnectionStrings:EstoqueConnection"]));
            services.AddScoped<CriarProdutosConsumer>();
            services.AddScoped<AtualizarProdutosConsumer>();

            // Registrar a fábrica
            services.AddSingleton<RabbitConsumerFactory>();

            return services;
        }
    }
}
