using Database.DAL.Entities.Base;

namespace Database.DAL.Entities
{
    #region AcceptingEnum

    public enum FriendshipState
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

        public FriendshipState ApplicationStateUserOne { get; set; }
        public FriendshipState ApplicationStateUserTwo { get; set; }

        public FriendshipType UserOneFriendshipType { get; set; }
        public FriendshipType UserTwoFriendshipType { get; set; }

        public bool IsFriend(int friendId) => IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
            FriendshipState.Allow, FriendshipState.Allow);

        public bool IsOutgoing(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipState.Suspence, FriendshipState.Allow)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipState.Allow, FriendshipState.Suspence);

        public bool IsIncoming(int friendId) => UserOne.Id.Equals(friendId)
            ? IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipState.Allow, FriendshipState.Suspence)
            : IsFitCondition(ApplicationStateUserOne, ApplicationStateUserTwo,
                FriendshipState.Suspence, FriendshipState.Allow);

        private bool IsFitCondition(FriendshipState friend, FriendshipState sender, FriendshipState resultFriend,
            FriendshipState resultSender) => friend == resultFriend && sender == resultSender;
        
    }
}
