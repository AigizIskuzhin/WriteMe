using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        public IEnumerable<User> Users => _Users.Items;
        public IEnumerable<Post> Posts => _Posts.Items;
        

        public IEnumerable<Post> GetUserPostsWithFilter(int id, string filterText) => Posts.Where(post => 
            post.OwnerId.Equals(id) && 
            (post.Title != null && post.Title.Contains(filterText) || 
             post.Description != null && post.Description.Contains(filterText)));

        public async Task<User> GetUserAsync(int id) => await _Users.GetAsync(id);

        public IEnumerable<Post> GetUserPosts(int id) => Posts.Where(post => post.OwnerId.Equals(id));
    }
}
