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
        private IRepository<UserPost> _Posts { get; }

        public ProfileService(IRepository<User> users, IRepository<UserPost> posts)
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
        public IEnumerable<UserPost> GetUserPostsWithFilter(int id, string filterText) => _Posts.Items.Where(post =>
            post.OwnerId.Equals(id) &&
            (post.Title != null && post.Title.Contains(filterText) ||
             post.Description != null && post.Description.Contains(filterText)))
            .OrderByDescending(post => post.CreatedDateTime);
        #endregion
            
        #region GetUserPosts

        /// <summary>
        /// Получить посты указанного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<UserPost> GetUserPosts(int id) => _Posts.Items
            .Where(post => post.OwnerId.Equals(id))
            .OrderByDescending(post => post.CreatedDateTime);
        #endregion

        #region GetUserAsync
        /// <summary>
        /// Получить указанного пользователя асинхронно 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(int id) => await _Users.GetAsync(id);
        #endregion

        #region UploadPost
        /// <summary>
        /// Добавление поста
        /// </summary>
        /// <param name="userPost"></param>
        /// <returns></returns>
        public UserPost UploadPost(UserPost userPost)
        {
            if (string.IsNullOrWhiteSpace(userPost.Title) && string.IsNullOrWhiteSpace(userPost.Description))
                return null;
            userPost.Owner = _Users.Get(userPost.OwnerId);
            return _Posts.Add(userPost);
        }
        #region Edit post
        /// <summary>
        /// Редактирование поста
        /// </summary>
        /// <param name="userPost"></param>
        /// <returns></returns>
        public UserPost EditPost(UserPost userPost)
        {
            var postDefault = _Posts.Get(userPost.Id);

            if (postDefault.OwnerId != userPost.OwnerId)
                return postDefault;

            postDefault.Title = userPost.Title;
            postDefault.Description = userPost.Description;

            _Posts.Update(postDefault);

            return postDefault;
        } 
        #endregion

        #endregion

        #region DeletePost
        /// <summary>
        /// Удаление поста по указанному id поста и id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemovePost(int idPost, int idUser)
        {
            var userPostExist = _Posts.Items.Any(post=>post.Id==idPost&&post.OwnerId==idUser);
            if(userPostExist)
                _Posts.Remove(idPost);
            return userPostExist;
        }
        #endregion
    }
}
