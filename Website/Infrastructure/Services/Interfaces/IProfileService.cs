using Database.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IProfileService
    {
        public UserPost UploadPost(UserPost userPost);
        public UserPost EditPost(UserPost userPost);
        public bool RemovePost(int idPost, int idUser);
        public IEnumerable<UserPost> GetUserPosts(int id);
        public IEnumerable<UserPost> GetUserPostsWithFilter(int id, string filterText);
        public Task<User> GetUserAsync(int id);
    }
}
