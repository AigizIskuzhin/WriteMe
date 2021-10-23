using System.Linq;
using Microsoft.EntityFrameworkCore;
using WriteMe.Database.DAL.Context;
using WriteMe.Database.DAL.Entities;

namespace WriteMe.Database.DAL
{
    class PostsRepository : DbRepository<Post>
    {
        public PostsRepository(WriteMeDatabase db):base(db)
        {

        }

        public override IQueryable<Post> Items => base.Items
            .Include(post => post.Owner);
    }
}
