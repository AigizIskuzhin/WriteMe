using Database.DAL.Entities.Base;

namespace Database.DAL.Entities
{
    public class FriendshipApplication : Entity
    {
        public User UserOne { get; set; }
        public User UserTwo { get; set; }

        public bool ApplicationStateUserOne { get; set; }
        public bool ApplicationStateUserTwo { get; set; }

        public FriendshipType UserOneFriendshipType { get; set; }
        public FriendshipType UserTwoFriendshipType { get; set; }

    }
}