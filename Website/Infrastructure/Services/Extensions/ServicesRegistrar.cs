using Microsoft.Extensions.DependencyInjection;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services.Extensions
{
    public static class ServicesRegistrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddTransient<IProfileService, ProfileService>()
            .AddTransient<IAuthenticateService, AuthenticateService>()
            .AddSingleton<ISignalRService, SignalRService>()
            .AddTransient<IMessengerService, MessengerService>()
            .AddTransient<IFriendsService, FriendsService>()
            .AddTransient<IPostingService, PostingService>()
            .AddTransient<IFileService, FileService>()

            .AddTransient<IUsersService, UsersService>();
    }
}
