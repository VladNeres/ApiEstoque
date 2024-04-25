using Aplication.Interface;
using Aplication.Service;
using SqlDataAccess.Repositories;
using SqlDataAccess.Repositories.Interface;

namespace ApiEstoque.DependenceInjecton
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static class DependenceInjection
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static IServiceCollection InjectionDependence(IServiceCollection services, IConfiguration configuration)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            //Services
            services.AddScoped<IEstoqueService, EstoqueService>();


            //Repository
#pragma warning disable CS8604 // Possible null reference argument.
            services.AddScoped<IEstoqueRepository, EstoqueRepository>(e => new EstoqueRepository(configuration["ConnectionStrings:EstoqueConnection"]));
#pragma warning restore CS8604 // Possible null reference argument.
            return services;
        }
    }
}
