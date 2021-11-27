using Database.DAL.Context;
using Database.DAL.Entities;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.DAL.Repositories
{
    class FriendshipApplicationsRepository: DbRepository<FriendshipApplication>
    {
       public FriendshipApplicationsRepository(WriteMeDatabase db) : base(db) { }
        public override IQueryable<FriendshipApplication> Items => base.Items
            .Include(friendship=>friendship.UserOne)
            .ThenInclude(u=>u.Role)
            .Include(friendship=>friendship.UserTwo)
            .ThenInclude(u=>u.Role);
    }
}
