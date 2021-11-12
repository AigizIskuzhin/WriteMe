using Database.DAL.Entities;
using Database.DAL.Entities.Chat;
using Database.DAL.Repositories.Base;
using Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Database.DAL.Repositories.ServicesRegistrator
{
    public static class RepositoriesRegistrator
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<User>, UsersRepository>()
            .AddTransient<IRepository<Role>, DbRepository<Role>>()
            .AddTransient<IRepository<Post>, PostsRepository>()
            .AddTransient<IRepository<Chat>, ChatsRepository>()
            .AddTransient<IRepository<ChatParticipant>, ChatsParticipantsRepository>()
            .AddTransient<IRepository<FriendshipApplication>, FriendshipApplicationsRepository>();
        //.AddTransient<IRepository<UserMessage>, IRepository<UserMessage>>()
        //.AddTransient<IRepository<GeneratedMessage>, IRepository<GeneratedMessage>>();
    }
}
