using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IChangeService, ChangeService>();
            
            return services;
        }
    }
}