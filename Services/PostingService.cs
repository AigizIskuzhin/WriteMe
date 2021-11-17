using Database.DAL.Entities;
using Database.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class PostingService : IPostingService
    {
        private readonly IRepository<SystemPost> SystemPostsRepository;
        private readonly IRepository<UserPost> UserPostsRepository;
        private readonly IRepository<PostReport> PostReportsRepository;
        private readonly IRepository<User> UsersRepository;
        private readonly IRepository<ReportType> ReportTypeRepository;

        public PostingService(
            IRepository<SystemPost> systemPostsRepository,
            IRepository<UserPost> userPostsRepository, 
            IRepository<User> usersRepository, 
            IRepository<PostReport> postReportsRepository, 
            IRepository<ReportType> reportTypeRepository)
        {
            SystemPostsRepository = systemPostsRepository;
            UserPostsRepository = userPostsRepository;
            UsersRepository = usersRepository;
            PostReportsRepository = postReportsRepository;
            ReportTypeRepository = reportTypeRepository;
        }

        public IEnumerable<SystemPost> GetSystemPosts()=> SystemPostsRepository.Items
            .OrderByDescending(post => post.CreatedDateTime);

        public IEnumerable<SystemPost> GetSystemPostsWithFilter(string filter) => SystemPostsRepository.Items
            .Where(post => (post.Title != null && post.Title.Contains(filter) || 
                           post.Description != null && post.Description.Contains(filter)))
            .OrderByDescending(post => post.CreatedDateTime);

        public SystemPost UploadPost(SystemPost systemPost)
        {
            if (string.IsNullOrWhiteSpace(systemPost.Title) && string.IsNullOrWhiteSpace(systemPost.Description))
                return null;
            return SystemPostsRepository.Add(systemPost);
        }

        public SystemPost EditPost(SystemPost systemPost)
        {
            var postDefault = SystemPostsRepository.Get(systemPost.Id);

            postDefault.Title = systemPost.Title;
            postDefault.Description = systemPost.Description;

            SystemPostsRepository.Update(postDefault);

            return postDefault;
        }

        public bool RemovePost(int idPost)
        { 
            UserPostsRepository.Remove(idPost);
            var post = PostReportsRepository.Get(idPost);
            return post==null;
        }

        public void SendReportToPost(int postId, int senderId, int reportTypeId, string msg)
        {
            var post = UserPostsRepository.Get(postId);
            var sender = UsersRepository.Get(senderId);
            if (PostReportsRepository.Items.Any(report => report.Post.Id.Equals(postId) && report.Sender.Id.Equals(senderId)))
                return;
            PostReportsRepository.Add(new()
            {
                Post = post,
                Sender = sender,
                ReportTypeId = reportTypeId,
                Commentary = msg,
                ReportStateId = 1
            });
        }

        public IEnumerable<PostReport> GetPostsReports() =>
            PostReportsRepository.Items.OrderByDescending(report => report.CreatedDateTime);

        public IEnumerable<ReportType> GetReportTypes() => ReportTypeRepository.Items;
        public void CloseReport(int reportId) => PostReportsRepository.Remove(reportId);

        public void CloseReportAndDeletePost(int reportId)
        {
            var report = PostReportsRepository.Get(reportId);
            RemovePost(report.Post.Id);
            PostReportsRepository.Remove(report.Id);
        }
    }
}
