using Database.DAL.Entities;
using Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
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
    }

    public class UsersService : IUsersService
    {
        private readonly IRepository<User> UsersRepository;

        public UsersService(IRepository<User> usersRepository)
        {
            UsersRepository = usersRepository;
        }

        public IEnumerable<PreviewProfileViewModel> GetUsersPreviews() =>
            from user in UsersRepository.Items select ConvertToPreviewVM(user);

        private static PreviewProfileViewModel ConvertToPreviewVM(User user) =>
            new()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Patronymic = user.Patronymic
            };

        public IEnumerable<PreviewProfileViewModel> GetUsersPreviews(string filter)
        {
            throw new System.NotImplementedException();
        }

        public PreviewProfileViewModel GetUserPreview(int id)
        {
            throw new System.NotImplementedException();
        }

        public PreviewProfileViewModel GetUserPreview(ProfileViewModel profile)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProfileViewModel> GetUsersProfiles()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProfileViewModel> GetUsersProfiles(string filter)
        {
            throw new System.NotImplementedException();
        }

        public ProfileViewModel GetUserProfileViewModel(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
