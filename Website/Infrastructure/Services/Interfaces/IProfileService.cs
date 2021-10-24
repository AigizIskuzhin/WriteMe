using System.Collections.Generic;
using System.Threading.Tasks;
using Database.DAL.Entities;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IProfileService
    {
        public IEnumerable<User> Users { get; }
        public IEnumerable<Post> GetUserPosts(int id);
        public IEnumerable<Post> GetUserPostsWithFilter(int id, string filterText);
        public Task<User> GetUserAsync(int id);
    }
}
