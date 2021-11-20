using System.Collections.Generic;
using Database.DAL.Entities;
using Website.ViewModels.Profile;

namespace Services.Interfaces
{
    public interface IPostingService
    {
        public IEnumerable<SystemPostViewModel> GetSystemPosts();
        public IEnumerable<SystemPostViewModel> GetSystemPostsWithFilter(string filter);
        public SystemPostViewModel UploadPost(SystemPostViewModel systemPost);
        public SystemPostViewModel EditPost(SystemPostViewModel systemPost);
        public bool RemovePost(int idPost);
        public void SendReportToPost(int postId, int senderId, int reportTypeId, string msg);
        public IEnumerable<PostReport> GetPostsReports();
        public IEnumerable<ReportType> GetReportTypes();
        public void CloseReport(int reportId);
        public void CloseReportAndDeletePost(int reportId);
    }
}
