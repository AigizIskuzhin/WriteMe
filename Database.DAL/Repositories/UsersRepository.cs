using System.Linq;
using Database.DAL.Context;
using Database.DAL.Entities;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Repositories
{
    class UsersRepository : DbRepository<User>
    {
        public UsersRepository(WriteMeDatabase db) : base(db)
        {

        }

        public override IQueryable<User> Items => base.Items
            .Include(user => user.Role)
            .Include(user=>user.Country);
    }
}
