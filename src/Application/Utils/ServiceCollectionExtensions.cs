using System.Diagnostics.CodeAnalysis;
using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Utils
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IChangeService, ChangeService>();
            services.AddSingleton<IOperationService, OperationService>();
            
            return services;
        }
    }
}