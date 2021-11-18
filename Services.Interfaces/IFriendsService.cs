using System.Collections.Generic;
using Database.DAL.Entities;
using Website.ViewModels.Friends;

namespace Services.Interfaces
{
    public interface IFriendsService
    {
        public FriendViewModel GetFriendViewModel(FriendshipApplication a, int userId,
            ApplicationState state = ApplicationState.friend);
        public IEnumerable<FriendViewModel> GetUserFriends(int userId);
        public IEnumerable<FriendViewModel> GetUserFriends(int userId, string filter);
        public IEnumerable<FriendViewModel> GetIncomingApplications(int userId);
        public IEnumerable<FriendViewModel> GetOutgoingApplications(int userId);
        public bool TryRemoveUserFriendship(int userId,int targetUserId);
        public bool TryRemoveOutgoingFriendship(int userId, int targetUserId);
        public bool TryAllowIncomingFriendship(int userId, int targetUserId);
        public bool TryDenyIncomingFriendship(int userId, int targetUserId);
        public bool TrySendFriendshipRequest(int userId, int targetUserId);
    }
}
