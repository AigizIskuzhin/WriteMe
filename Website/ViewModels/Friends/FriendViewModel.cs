using Database.DAL.Entities;
using Website.Infrastructure.Services;

namespace Website.ViewModels.Friends
{
    public class FriendViewModel
    {
        public int Id;
        public string Name;
        public string PhotoPath;

        public string FriendshipType;
        public FriendshipApplication FriendshipApplication { get; set; }
        public bool IsOutgoing { get; set; } = false;
        public bool IsIncoming { get; set; } = false;
        public FriendsService.state State { get; set; }
    }
}
