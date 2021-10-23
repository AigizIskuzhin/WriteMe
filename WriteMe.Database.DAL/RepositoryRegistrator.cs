using Microsoft.Extensions.DependencyInjection;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

namespace WriteMe.Database.DAL
{
    public static class RepositoryRegistrator
    {
        public static IServiceCollection AddRepositoriesInDB(this IServiceCollection services) => services
            .AddTransient<IRepository<User>, UsersRepository>()
            .AddTransient<IRepository<Role>, DbRepository<Role>>()
            .AddTransient<IRepository<Post>, PostsRepository>();
    }
}
