using System.Linq;
using Database.DAL.Context;
using Database.DAL.Entities;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Repositories
{
    class PostReportsRepository : DbRepository<PostReport>
    {
        public PostReportsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<PostReport> Items => base.Items
            .Include(report => report.Sender)
            .Include(report=> report.Post)
            .ThenInclude(post=>post.Owner)
            .Include(report=>report.ReportType)
            .Include(report=>report.ReportState);
    }
}
