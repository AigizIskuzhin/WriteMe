using System.Linq;
using Microsoft.EntityFrameworkCore;
using WriteMe.Database.DAL.Context;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.DAL.Repositories.Base;

namespace WriteMe.Database.DAL.Repositories
{
    class UsersRepository : DbRepository<User>
    {
        public UsersRepository(WriteMeDatabase db) : base(db)
        {

        }

        public override IQueryable<User> Items => base.Items
            .Include(user => user.Role);
    }
}
