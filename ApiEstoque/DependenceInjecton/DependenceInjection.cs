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

        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            // Registrar os serviços e consumidores
            services.AddScoped<IEstoqueService, EstoqueService>();
            services.AddScoped<IEstoqueRepository, EstoqueRepository>(e => new EstoqueRepository(configuration["ConnectionStrings:EstoqueConnection"]));

           
           
            return services;
        }

        
    }
}
