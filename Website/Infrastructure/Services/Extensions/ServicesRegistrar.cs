using Microsoft.Extensions.DependencyInjection;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services.Extensions
{
    public static class ServicesRegistrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IProfileService,ProfileService>()
            .AddTransient<IAuthenticateService, AuthenticateService>()
            .AddTransient<IMessengerService, MessengerService>()
            .AddTransient<IFriendsService, FriendsService>()
            .AddSingleton<ISignalRService, SignalRService>()
            .AddTransient<IPostingService, PostingService>();
    }
}
