using Database.DAL.Entities;
using Database.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Website.ViewModels;
using Website.ViewModels.Profile;

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

        private SystemPostViewModel GetSystemPostViewModel(SystemPost p) => p != null
            ? new SystemPostViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreationDateTime = p.CreatedDateTime
            }
            : null;


        public bool IsAdmin(int userId)
        {
            var user = UsersRepository.Get(userId);
            return user.Role.Id == 3;
        }

        public IEnumerable<SystemPostViewModel> GetSystemPosts()
        {
            foreach (var p in SystemPostsRepository.Items)
                yield return GetSystemPostViewModel(p);
        }

        public IEnumerable<SystemPostViewModel> GetSystemPostsWithFilter(string filter) =>
            (from p in SystemPostsRepository.Items.Where(post => post.Title != null && post.Title.Contains(filter) ||
                                                                 post.Description != null && post.Description.Contains(filter))
                select GetSystemPostViewModel(p))
            .OrderByDescending(post => post.CreationDateTime);

        public SystemPostViewModel UploadPost(SystemPostViewModel systemPost)
        {
            if (string.IsNullOrWhiteSpace(systemPost.Title) && string.IsNullOrWhiteSpace(systemPost.Description))
                return null;
            var t = SystemPostsRepository.Add(new SystemPost()
            {
                Title = systemPost.Title,
                Description = systemPost.Description
            });
            return GetSystemPostViewModel(t);
        }

        public SystemPostViewModel EditPost(SystemPostViewModel systemPost)
        {
            var postDefault = SystemPostsRepository.Get(systemPost.Id);

            postDefault.Title = systemPost.Title;
            postDefault.Description = systemPost.Description;

            SystemPostsRepository.Update(postDefault);

            return GetSystemPostViewModel(postDefault);
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

        public IEnumerable<PostReportViewModel> GetPostsReports() => from report in
            PostReportsRepository.Items.OrderByDescending(report => report.CreatedDateTime) select new PostReportViewModel
        {
            Id = report.Id,
            PostId = report.PostId
        };

        public IEnumerable<ReportTypeVM> GetReportTypes() => from report in ReportTypeRepository.Items select new ReportTypeVM
        {
            Id = report.Id,
            Name = report.Name
        };
        public void CloseReport(int reportId) => PostReportsRepository.Remove(reportId);

        public void CloseReportAndDeletePost(int reportId)
        {
            var report = PostReportsRepository.Get(reportId);
            RemovePost(report.Post.Id);
            PostReportsRepository.Remove(report.Id);
        }
    }
}
