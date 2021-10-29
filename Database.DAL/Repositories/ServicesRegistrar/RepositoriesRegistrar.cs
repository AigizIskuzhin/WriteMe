using Database.DAL.Entities;
using Database.DAL.Entities.Chat.GroupChat;
using Database.DAL.Entities.Chat.PrivateChat;
using Database.DAL.Repositories.Base;
using Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Database.DAL.Repositories.ServicesRegistrar
{
    public static class RepositoriesRegistrar
    {
        public static IServiceCollection AddRepositoriesInDb(this IServiceCollection services) => services
            .AddTransient<IRepository<User>, UsersRepository>()
            .AddTransient<IRepository<Role>, DbRepository<Role>>()
            .AddTransient<IRepository<Post>, PostsRepository>()
            .AddTransient<IRepository<PrivateChat>, PrivateChatsRepository>()
            .AddTransient<IRepository<GroupChat>, GroupChatsRepository>()
            //.AddTransient<IRepository<GroupChatParticipant>, GroupChatsParticipantsRepository>()
            .AddTransient<IRepository<FriendshipApplication>, FriendshipApplicationsRepository>();
        //.AddTransient<IRepository<UserMessage>, IRepository<UserMessage>>()
        //.AddTransient<IRepository<GeneratedMessage>, IRepository<GeneratedMessage>>();
    }
}
