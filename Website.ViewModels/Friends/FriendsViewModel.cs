using System.Collections.Generic;
using Website.ViewModels.Base;
using Website.ViewModels.Users;

namespace Website.ViewModels.Friends
{
    public class FriendsViewModel
    {
        public bool IsOwner { get; set; }
        public IEnumerable<FriendViewModel> Friends { get; set; }
    }

    public class FriendshipApplicationVM : EntityViewModel
    {
        public UserViewModel UserOne { get; set; }
        public UserViewModel UserTwo { get; set; }

        public FriendshipStatesVM ApplicationStateUserOne { get; set; }
        public FriendshipStatesVM ApplicationStateUserTwo { get; set; }
        
        public bool IsFriend(int friendId) => IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
            FriendshipStatesVM.Allow, FriendshipStatesVM.Allow);

        public bool IsOutgoing(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStatesVM.Suspence, FriendshipStatesVM.Allow)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStatesVM.Allow, FriendshipStatesVM.Suspence);

        public bool IsIncoming(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStatesVM.Allow, FriendshipStatesVM.Suspence)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStatesVM.Suspence, FriendshipStatesVM.Allow);

        private bool IsFitCondition(FriendshipStatesVM friend, FriendshipStatesVM sender, FriendshipStatesVM resultFriend,
            FriendshipStatesVM resultSender) => friend == resultFriend && sender == resultSender;
    }

    public enum FriendshipStatesVM
    {
        Suspence=0,
        Allow=1,
        Deny=2
    }
}
