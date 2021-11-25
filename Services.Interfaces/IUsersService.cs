using System.Collections.Generic;
using Website.ViewModels;
using Website.ViewModels.Users;

namespace Services.Interfaces
{
    public interface IUsersService
    {
        public IEnumerable<PreviewProfileViewModel> GetUsersPreviews();
        public IEnumerable<PreviewProfileViewModel> GetUsersPreviews(string filter);
        public PreviewProfileViewModel GetUserPreview(int id);
        public PreviewProfileViewModel GetUserPreview(ProfileViewModel profile);

        public IEnumerable<ProfileViewModel> GetUsersProfiles();
        public IEnumerable<ProfileViewModel> GetUsersProfiles(string filter);
        public ProfileViewModel GetUserProfileViewModel(int id);

        public IEnumerable<UserViewModel> GetUsers();
    }
}
