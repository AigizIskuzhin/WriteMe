using System.Collections.Generic;
using System.Threading.Tasks;
using Website.Infrastructure.Services.Interfaces;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.Interfaces;

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

        private IEnumerable<Post> GetUserPosts(int id)
        {
            foreach (var post in _Posts.Items)
            {
                if (post.OwnerId == id) yield return post;
            }
        }
        public IEnumerable<User> Users => _Users.Items;

        public IEnumerable<Post> GetPosts(int id)
        {
            foreach (var post in _Posts.Items)
                if (post.OwnerId == id) yield return post;
            
        }
        public Task<User> GetUserAsync(int id) => _Users.GetAsync(id);
    }
}
