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

    }
}