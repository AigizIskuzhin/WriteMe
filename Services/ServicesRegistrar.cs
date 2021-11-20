using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Services
{
    public static class ServicesRegistrar
    {
        public static IServiceCollection AddServicesLibrary(this IServiceCollection services) => services
            .AddTransient<IProfileService, ProfileService>()
            .AddTransient<IAuthenticateService, AuthenticateService>()
            .AddTransient<IMessengerService, MessengerService>()
            .AddTransient<IFriendsService, FriendsService>()
            .AddTransient<IPostingService, PostingService>()
            .AddTransient<IFileService, FileService>()
            .AddTransient<IUsersService, UsersService>();
    }
}
