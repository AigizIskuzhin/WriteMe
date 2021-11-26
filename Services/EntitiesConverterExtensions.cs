using Database.DAL.Entities;
using Website.ViewModels;
using Website.ViewModels.Users;

namespace Services
{
    public static class EntitiesConverterExtensions
    {
        public static UserViewModel GetViewModel(this User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            Birthday = user.Birthday,
            AvatarPath = user.AvatarPath,
            Country = user.Country.Name,
            AccessLevel = user.Role.Code.Equals("user")?AccessLevel.user:user.Role.Code.Equals("mod")?AccessLevel.mod:AccessLevel.admin
        };

        public static PreviewProfileViewModel GetPreviewViewModel(this User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            AvatarPath = user.AvatarPath
        };

        public static UserPostViewModel GetViewModel(this UserPost p) => new()
        {
            Id = p.Id,
            CreatedDateTime = p.CreatedDateTime,
            Description = p.Description,
            Title = p.Title,
            Owner = GetViewModel(p.Owner)
        };
    }
}
