using System.Collections.Generic;
using System.Threading.Tasks;
using Website.ViewModels;
using Website.ViewModels.Users;

namespace Services.Interfaces
{
    public interface IProfileService
    {
        public UserPostViewModel UploadPost(UserPostViewModel userPost);
        public UserPostViewModel EditPost(UserPostViewModel userPost);
        public bool RemovePost(int idPost, int idUser);
        public IEnumerable<UserPostViewModel> GetUserPosts(int id);
        public IEnumerable<UserPostViewModel> GetUserPostsWithFilter(int id, string filterText);
        public Task<UserViewModel> GetUserAsync(int id);
    }
}
