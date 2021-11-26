using System;
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

        public FriendshipStateVM ApplicationStateUserOne { get; set; }
        public FriendshipStateVM ApplicationStateUserTwo { get; set; }

        public ApplicationState GetApplicationState(int userId) =>
            ApplicationStateUserOne switch
            {
                FriendshipStateVM.Allow when ApplicationStateUserTwo == FriendshipStateVM.Allow => ApplicationState
                    .friend,
                FriendshipStateVM.Allow when ApplicationStateUserTwo == FriendshipStateVM.Suspence =>
                    UserOne.Id == userId ? ApplicationState.outgoing : ApplicationState.incoming,
                FriendshipStateVM.Suspence when ApplicationStateUserTwo == FriendshipStateVM.Allow =>
                    UserOne.Id == userId ? ApplicationState.incoming : ApplicationState.outgoing,
                FriendshipStateVM.Deny => throw new ArgumentOutOfRangeException(),
                _ => throw new ArgumentOutOfRangeException()
            };

        public bool IsFriend(int friendId) => IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
            FriendshipStateVM.Allow, FriendshipStateVM.Allow);

        public bool IsOutgoing(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStateVM.Suspence, FriendshipStateVM.Allow)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStateVM.Allow, FriendshipStateVM.Suspence);

        public bool IsIncoming(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStateVM.Allow, FriendshipStateVM.Suspence)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStateVM.Suspence, FriendshipStateVM.Allow);

        private bool IsFitCondition(FriendshipStateVM friend, FriendshipStateVM sender, FriendshipStateVM resultFriend,
            FriendshipStateVM resultSender) => friend == resultFriend && sender == resultSender;
    }

    public enum FriendshipStateVM
    {
        Suspence=0,
        Allow=1,
        Deny=2
    }
}
