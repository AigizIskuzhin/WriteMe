using Database.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services.Extensions
{
    public static class IConverterServicesRegistrar
    {
        public static IServiceCollection AddIConverterServices(this IServiceCollection services) => services
            .AddTransient<IConverterService<User>, UserConverter>();
    }
}
