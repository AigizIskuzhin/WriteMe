using Microsoft.Extensions.DependencyInjection;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IProfileService,ProfileService>()
            .AddTransient<IAuthenticateService, AuthenticateService>()
            .AddTransient<IFriendsService, FriendsService>();
    }
}
