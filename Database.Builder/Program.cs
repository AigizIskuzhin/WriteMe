using System;
using System.Threading.Tasks;
using Database.DAL.Context;
using Database.DAL.Entities;
using Database.DAL.Entities.Chat;
using Database.DAL.Repositories.ServicesRegistrar;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Database.Builder
{
    class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>Host.CreateDefaultBuilder(args)
            .UseContentRoot(Environment.CurrentDirectory)
            .ConfigureServices(services =>services
                .AddDbContext<WriteMeDatabase>(opt => 
                    opt.UseMySql("server=localhost;port=3306;database=writemedatabaseTest;uid=root;password=admin",
                        new MySqlServerVersion(new Version(8, 0, 26)))
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                        .EnableServiceProviderCaching())
                .AddRepositoriesInDb()
                .AddTransient<WriteMeDatabaseTestInitializer>());

        static Task Main(string[] args)
        {
            
            using IHost host = CreateHostBuilder(args).Build();

            //ExemplifyScoping(host.Services, "Scope 1");
            //ExemplifyScoping(host.Services, "Scope 2");
            Test(host.Services);
            return host.RunAsync();
        }

        private static void Test(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            // Инициализация тестовых данных, которых еще нет.
            provider.GetRequiredService<WriteMeDatabaseTestInitializer>().InitializeAsync().Wait();
            // Удаление бд, миграция моделей, создание бд (в будущем инициализация данных еще)

            //IRepository<Chat> chatRepository = provider.GetRequiredService<IRepository<Chat>>();
            IRepository<User> usersRepository = provider.GetRequiredService<IRepository<User>>();

            
            // Можешь вручную создавать пока что
            usersRepository.Add(
                new User()
                {
                    Name = ""
                });


        }
        
    }
}
