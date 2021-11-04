using System.Collections.Generic;
using System.Linq;
using Database.DAL.Entities;
using Database.Interfaces;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public class PostingService : IPostingService
    {
        private readonly IRepository<SystemPost> SystemPostsRepository;

        public PostingService(IRepository<SystemPost> systemPostsRepository)
        {
            SystemPostsRepository = systemPostsRepository;
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
            var userPostExist = SystemPostsRepository.Items.Any(post=>post.Id==idPost);
            if(userPostExist)
                SystemPostsRepository.Remove(idPost);
            return userPostExist;
        }
    }
}
