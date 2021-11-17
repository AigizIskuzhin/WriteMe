using System.Collections.Generic;
using Database.DAL.Entities;

namespace Services.Interfaces
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
        public void CloseReport(int reportId);
        public void CloseReportAndDeletePost(int reportId);
    }
}
