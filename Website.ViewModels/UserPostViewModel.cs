using System;
using Website.ViewModels.Profile;
using Website.ViewModels.Users;

namespace Website.ViewModels
{
    public class UserPostViewModel: PostViewModel
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int OwnerId { get; set; }
        public UserViewModel Owner { get; set; }

        public bool IsOwner { get; set; }
        public bool IsMod { get; set; }
    }
}
