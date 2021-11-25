using Microsoft.Extensions.DependencyInjection;

namespace Website.Infrastructure.SignalRHubs
{
    public static class ServicesRegistrar
    {
        public static IServiceCollection AddHubServices(this IServiceCollection services) => services
            .AddTransient<ISignalRService, SignalRService>();
    }
}
