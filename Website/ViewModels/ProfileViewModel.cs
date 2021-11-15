using Database.DAL.Entities;
using System.Collections.Generic;
using Website.Infrastructure.Extensions;

namespace Website.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName => (User.Name + " " + User.Surname + " " + User.Patronymic).Replace("  ", " ");
        public User User { get; set; }
        public string UserAge => User.Birthday.ToAgeString();
        public bool IsOwner { get; set; }
        public bool IsMod { get; set; }
        public bool IsFriend { get; set; }
        public IEnumerable<UserPost> UserPosts { get; set; }
        public bool IsNew => true; //User.IsNew;
        public bool IsPostCreating { get; set; }
    }
}
