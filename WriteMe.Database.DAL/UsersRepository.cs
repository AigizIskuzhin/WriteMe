using Microsoft.EntityFrameworkCore;
using System.Linq;
using WriteMe.Database.DAL.Context;
using WriteMe.Database.DAL.Entities;

namespace WriteMe.Database.DAL
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
