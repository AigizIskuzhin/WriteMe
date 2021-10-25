using Database.DAL.Entities;
using Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private IRepository<User> _Users { get; }
        private IRepository<Post> _Posts { get; }
        public ProfileService(IRepository<User> users, IRepository<Post> posts)
        {
            _Users = users;
            _Posts = posts;
        }

        #region GetUserPostsWithFilter
        /// <summary>
        /// Получить посты указанного пользователя, у которых заголовок или текст содержит текст фильтра
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filterText"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetUserPostsWithFilter(int id, string filterText) => _Posts.Items.Where(post =>
            post.OwnerId.Equals(id) &&
            (post.Title != null && post.Title.Contains(filterText) ||
             post.Description != null && post.Description.Contains(filterText)))
            .OrderByDescending(post => post.CreationDateTime);
        #endregion
            
        #region GetUserPosts

        public Post UploadPost(Post post)
        {
            if (string.IsNullOrWhiteSpace(post.Title) && string.IsNullOrWhiteSpace(post.Description))
                return null;
            if (post.Owner == null)
                return null;
            return _Posts.Add(post);
        }

        /// <summary>
        /// Получить посты указанного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetUserPosts(int id) => _Posts.Items
            .Where(post => post.OwnerId.Equals(id))
            .OrderByDescending(post => post.CreationDateTime);
        #endregion

        #region GetUserAsync
        /// <summary>
        /// Получить указанного пользователя асинхронно 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(int id) => await _Users.GetAsync(id); 
        #endregion

    }
}
