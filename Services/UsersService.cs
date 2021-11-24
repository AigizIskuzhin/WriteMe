using System.Collections.Generic;
using System.Linq;
using Database.DAL.Entities;
using Database.Interfaces;
using Services.Interfaces;
using Website.ViewModels;
using Website.ViewModels.Users;

namespace Services
{
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
                Patronymic = user.Patronymic,
                AvatarPath = user.AvatarPath
            };

        public IEnumerable<PreviewProfileViewModel> GetUsersPreviews(string filter) =>
            from user in UsersRepository.Items.Where(u =>
                u.Name.Contains(filter) || u.Surname.Contains(filter) || u.Patronymic.Contains(filter))
            select ConvertToPreviewVM(user);

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

        public IEnumerable<UserViewModel> GetUsers() => from user in UsersRepository.Items select GetViewModel(user);
            
        public static UserViewModel GetViewModel(User u) => new()
        {
            Id=u.Id,
            Name = u.Name,
            Surname = u.Surname,
            Patronymic = u.Patronymic,
            Birthday = u.Birthday,
            AvatarPath = u.AvatarPath
        };
    }
}