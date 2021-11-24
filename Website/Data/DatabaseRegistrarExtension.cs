using System;
using Database.DAL.Context;
using Database.DAL.Repositories.ServicesRegistrar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Website.Data
{
    static class DatabaseRegistrarExtenstion
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration Configuration) => services
            .AddDbContext<WriteMeDatabase>(opt =>
            {
                var type = Configuration["Type"];
                switch (type)
                {
                    case null: throw new InvalidOperationException("Не определён тип БД");
                    default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

                    case "MSSQL":
                        throw new NotImplementedException();
                        opt.UseSqlServer(Configuration.GetConnectionString(type));
                        break;
                    case "SQLite":
                        throw new NotImplementedException();
                        opt.UseSqlite(Configuration.GetConnectionString(type));
                        break;
                    case "MySql":
                        opt.UseMySql(Configuration.GetConnectionString(type), new MySqlServerVersion(new Version(8,0,26)));
                        break;
                    case "InMemory":
                        throw new NotImplementedException();
                        opt.UseInMemoryDatabase("Bookinist.db");
                        break;
                }
            })
            //.AddTransient<DbInitializer>()
            .AddRepositoriesInDb()
        ;
    }
}