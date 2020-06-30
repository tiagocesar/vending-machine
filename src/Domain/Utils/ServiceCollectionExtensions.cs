using System.Diagnostics.CodeAnalysis;
using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IProductGrid, ProductGrid>();
            services.AddSingleton<ICustomerCoinStack, CustomerCoinStack>();
            services.AddSingleton<IMachineCoinStack, MachineCoinStack>();

            return services;
        }
    }
}