using Database.DAL.Entities;
using Database.DAL.Entities.Base;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.ChatMessage;
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
            .AddTransient<IRepository<Country>, DbRepository<Country>>()
            .AddTransient<IRepository<UserPost>, PostsRepository>()
            .AddTransient<IRepository<SystemPost>, DbRepository<SystemPost>>()
            .AddTransient<IRepository<Chat>, ChatsRepository>()
            .AddTransient<IRepository<ChatParticipant>, ChatParticipantRepository>()
            .AddTransient<IRepository<PostReport>, PostReportsRepository>()
            .AddTransient<IRepository<ReportType>, DbRepository<ReportType>>()
            .AddTransient<IRepository<ReportState>, DbRepository<ReportState>>()
            //.AddTransient<IRepository<GroupChatParticipant>, GroupChatsParticipantsRepository>()
            .AddTransient<IRepository<FriendshipApplication>, FriendshipApplicationsRepository>();
        //.AddTransient<IRepository<UserMessage>, IRepository<UserMessage>>()
        //.AddTransient<IRepository<GeneratedMessage>, IRepository<GeneratedMessage>>();
    }
}
