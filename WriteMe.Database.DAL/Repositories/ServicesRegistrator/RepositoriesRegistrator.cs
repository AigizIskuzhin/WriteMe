using Microsoft.Extensions.DependencyInjection;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.DAL.Entities.Chat;
using WriteMe.Database.DAL.Repositories.Base;
using WriteMe.Database.Interfaces;

namespace WriteMe.Database.DAL.Repositories.ServicesRegistrator
{
    public static class RepositoriesRegistrator
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<User>, UsersRepository>()
            .AddTransient<IRepository<Role>, DbRepository<Role>>()
            .AddTransient<IRepository<Post>, PostsRepository>()
            .AddTransient<IRepository<Chat>, ChatsRepository>()
            .AddTransient<IRepository<ChatParticipant>, ChatsParticipantsRepository>();
        //.AddTransient<IRepository<UserMessage>, IRepository<UserMessage>>()
        //.AddTransient<IRepository<GeneratedMessage>, IRepository<GeneratedMessage>>();
    }
}
