using Database.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.ViewModels.Friends;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IFriendsService
    {
        public IEnumerable<FriendViewModel> GetUserFriends(int userId);
        public IEnumerable<FriendViewModel> GetUserFilteredFriends(int userId,string filterString);
        public bool TryRemoveUserFriendship(int userId,int targetUserId);
        public IEnumerable<FriendViewModel> GetUserIncomingFriendships(int userId);
        public IEnumerable<FriendViewModel> GetUserOutgoingFriendships(int userId);
        public bool TryRemoveOutgoingFriendship(int userId, int targetUserId);
        //public bool TryAllowIncomingFriendship(int userId, int targetUserId);
        //public bool TryDenyIncomingFriendship(int userId, int targetUserId);
        public bool TryResponseIncomingFriendship(int userId, int targetUserId, FriendshipStates responseState);
    }
}
