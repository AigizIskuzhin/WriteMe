using System.Collections.Generic;

namespace Website.ViewModels.Friends
{
    public class FriendsViewModel
    {
        public bool IsOwner { get; set; }
        public IEnumerable<FriendViewModel> Friends { get; set; }
    }
}
