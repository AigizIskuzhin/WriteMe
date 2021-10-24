using System.Linq;
using Database.DAL.Context;
using Database.DAL.Entities;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Repositories
{
    class PostsRepository : DbRepository<Post>
    {
        public PostsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<Post> Items => base.Items
            .Include(post => post.Owner);
    }
}
