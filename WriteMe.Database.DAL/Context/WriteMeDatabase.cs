using Microsoft.EntityFrameworkCore;
using WriteMe.Database.DAL.Entities;

namespace WriteMe.Database.DAL.Context
{
    public class WriteMeDatabase : DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public WriteMeDatabase(DbContextOptions<WriteMeDatabase> options) : base(options)
        {

        }
    }
}
