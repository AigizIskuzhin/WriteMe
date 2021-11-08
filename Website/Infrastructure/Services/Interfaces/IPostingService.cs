using Database.DAL.Entities;
using System.Collections.Generic;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IPostingService
    {
        public IEnumerable<SystemPost> GetSystemPosts();
        public IEnumerable<SystemPost> GetSystemPostsWithFilter(string filter);
        public SystemPost UploadPost(SystemPost systemPost);
        public SystemPost EditPost(SystemPost systemPost);
        public bool RemovePost(int idPost);
        public void SendReportToPost(int postId, int senderId, int reportTypeId, string msg);
        public IEnumerable<PostReport> GetPostsReports();
        public IEnumerable<ReportType> GetReportTypes();
    }
}
