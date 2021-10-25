using System.Collections.Generic;
using System.Threading.Tasks;
using Database.DAL.Entities;
using Website.ViewModels.Profile;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IProfileService
    {
        public Post UploadPost(Post post);
        public bool RemovePost(int idPost, int idUser);
        public IEnumerable<Post> GetUserPosts(int id);
        public IEnumerable<Post> GetUserPostsWithFilter(int id, string filterText);
        public Task<User> GetUserAsync(int id);
    }
}
