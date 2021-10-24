using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WriteMe.Database.DAL.Context;
using WriteMe.Database.DAL.Entities;

namespace WriteMe.Database.Builder
{
    class WriteMeDatabaseTestInitializer
    {
        private readonly WriteMeDatabase Database;
        private readonly ILogger<WriteMeDatabaseTestInitializer> Logger;
        public WriteMeDatabaseTestInitializer(WriteMeDatabase database, ILogger<WriteMeDatabaseTestInitializer> logger)
        {
            Database = database;
            Logger = logger;
        }

        public void Initialize()
        {
            Database.Database.EnsureDeleted();
            Database.Database.Migrate();
        }
        public async Task InitializeAsync()
        {
            await Database.Database.EnsureDeletedAsync().ConfigureAwait(false);
            await Database.Database.MigrateAsync();

            if(await Database.Users.AnyAsync())return;
        }
        
        private async Task InitializeRoles()
        {
             
        }
    }
}
