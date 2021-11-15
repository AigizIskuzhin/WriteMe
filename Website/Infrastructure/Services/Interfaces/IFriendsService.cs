using System.Collections.Generic;
using Website.ViewModels.Friends;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IFriendsService
    {
        public FriendViewModel GetFriendViewModel(int userId, int targetUserId);
        public IEnumerable<FriendViewModel> GetUserFriends(int userId);
        public IEnumerable<FriendViewModel> GetUserFilteredFriends(int userId,string filterString);
        public IEnumerable<FriendViewModel> GetUserIncomingFriendships(int userId);
        public IEnumerable<FriendViewModel> GetUserOutgoingFriendships(int userId);
        public bool TryRemoveUserFriendship(int userId,int targetUserId);
        public bool TryRemoveOutgoingFriendship(int userId, int targetUserId);
        public bool TryAllowIncomingFriendship(int userId, int targetUserId);
        public bool TryDenyIncomingFriendship(int userId, int targetUserId);
        public bool TrySendFriendshipRequest(int userId, int targetUserId);
    }
}
