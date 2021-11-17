using System.Collections.Generic;
using Website.ViewModels.Friends;
using Website.ViewModels.Users;

namespace Website.ViewModels
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string FullName => (User.Name + " " + User.Surname + " " + User.Patronymic).Replace("  ", " ");
        public UserViewModel User { get; set; }
        public string UserAge => User.Birthday.ToAgeString();
        public bool IsOwner { get; set; }
        public bool IsMod { get; set; }
        public bool IsFriend { get; set; }
        public IEnumerable<UserPostViewModel> UserPosts { get; set; }
        public bool IsNew => true; //User.IsNew;
        public bool IsPostCreating { get; set; }
        public FriendViewModel FriendViewModel { get; set; }
    }
}
