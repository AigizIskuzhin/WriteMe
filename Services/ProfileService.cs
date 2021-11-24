using Database.DAL.Entities;
using Database.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.ViewModels;
using Website.ViewModels.Users;

namespace Services
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
        public IEnumerable<UserPostViewModel> GetUserPostsWithFilter(int id, string filterText) => from p in _Posts
                .Items.Where(post => post.OwnerId.Equals(id) &&
                                     (post.Title != null && post.Title.Contains(filterText) ||
                                      post.Description != null && post.Description.Contains(filterText)))
                .OrderByDescending(post => post.CreatedDateTime)
            select GetViewModel(p);
        #endregion
            
        #region GetUserPosts

        /// <summary>
        /// Получить посты указанного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<UserPostViewModel> GetUserPosts(int id) => from p in _Posts.Items
                .Where(post => post.OwnerId.Equals(id))
                .OrderByDescending(post => post.CreatedDateTime)
            select GetViewModel(p);
        #endregion

        #region GetUserAsync
        /// <summary>
        /// Получить указанного пользователя асинхронно 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserViewModel> GetUserAsync(int id)
        {
            var u = await _Users.GetAsync(id);
            return GetViewModel(u);
        }

        #endregion

        #region UploadPost
        /// <summary>
        /// Добавление поста
        /// </summary>
        /// <param name="userPost"></param>
        /// <returns></returns>
        public UserPostViewModel UploadPost(UserPostViewModel userPost)
        {
            if (string.IsNullOrWhiteSpace(userPost.Title) && string.IsNullOrWhiteSpace(userPost.Description))
                return null;
            var post = new UserPost
            {
                Owner = _Users.Get(userPost.OwnerId),
                Title = userPost.Title,
                Description = userPost.Description
            };
            var p = _Posts.Add(post);
            return p != null ? GetViewModel(p) : null;
        }

        #endregion
        
        #region Edit post
        /// <summary>
        /// Редактирование поста
        /// </summary>
        /// <param name="userPost"></param>
        /// <returns></returns>
        public UserPostViewModel EditPost(UserPostViewModel userPost)
        {
            var postDefault = _Posts.Get(userPost.Id);

            if (postDefault.OwnerId != userPost.OwnerId)
                return GetViewModel(postDefault);

            postDefault.Title = userPost.Title;
            postDefault.Description = userPost.Description;

            _Posts.Update(postDefault);

            return GetViewModel(postDefault);
        } 
        #endregion

        public static UserPostViewModel GetViewModel(UserPost p) => new()
        {
            Id = p.Id,
            CreatedDateTime = p.CreatedDateTime,
            Description = p.Description,
            Title = p.Title,
            Owner = GetViewModel(p.Owner)
        };
        public static UserViewModel GetViewModel(User u) => u!=null?new UserViewModel
        {
            Id=u.Id,
            Name = u.Name,
            Surname = u.Surname,
            Patronymic = u.Patronymic,
            Birthday = u.Birthday,
            AvatarPath = u.AvatarPath,
            MailAddress = u.MailAddress,
            Country = u.Country.Name
        }:null;

        #region RemovePost
        /// <summary>
        /// Удаление поста по указанному id поста и id пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemovePost(int idPost, int idUser)
        {
            var post = _Posts.Get(idPost);
            if (post.OwnerId.Equals(idUser))
            {
                post = null;
                _Posts.Remove(idPost);
            }
            return post==null;
        }
        #endregion
    }
}
