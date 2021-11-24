using Database.DAL.Entities.Base;

namespace Database.DAL.Entities
{
    #region AcceptingEnum

    public enum FriendshipStates
    {
        Suspence=0,
        Allow=1,
        Deny=2
    }

    #endregion
    public class FriendshipApplication : Entity
    {
        public User UserOne { get; set; }
        public User UserTwo { get; set; }

        public FriendshipStates ApplicationStateUserOne { get; set; }
        public FriendshipStates ApplicationStateUserTwo { get; set; }

        public FriendshipType UserOneFriendshipType { get; set; }
        public FriendshipType UserTwoFriendshipType { get; set; }

        public bool IsFriend(int friendId) => IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
            FriendshipStates.Allow, FriendshipStates.Allow);

        public bool IsOutgoing(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStates.Suspence, FriendshipStates.Allow)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStates.Allow, FriendshipStates.Suspence);

        public bool IsIncoming(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStates.Allow, FriendshipStates.Suspence)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipStates.Suspence, FriendshipStates.Allow);

        private bool IsFitCondition(FriendshipStates friend, FriendshipStates sender, FriendshipStates resultFriend,
            FriendshipStates resultSender) => friend == resultFriend && sender == resultSender;
        
    }
}
