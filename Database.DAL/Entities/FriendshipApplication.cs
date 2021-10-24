using Database.DAL.Entities.Base;

namespace Database.DAL.Entities
{
    public class FriendshipApplication : Entity
    {
        public int UserOneId { get; set; }
        public int UserTwoId { get; set; }


        public User UserOne { get; set; }
        public User UserTwo { get; set; }

        public bool ApplicationStateUserOne { get; set; }
        public bool ApplicationStateUserTwo { get; set; }
    }
}
