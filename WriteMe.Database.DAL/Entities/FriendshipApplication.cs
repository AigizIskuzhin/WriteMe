using WriteMe.Database.DAL.Entities.Base;

namespace WriteMe.Database.DAL.Entities
{
    public class FriendshipApplication : Entity
    {
<<<<<<< HEAD
        public int UserOneId { get; set; }
=======
        public int UserOneId{get; set; }
>>>>>>> dev
        public int UserTwoId { get; set; }


        public User UserOne { get; set; }
        public User UserTwo { get; set; }

        public bool ApplicationStateUserOne { get; set; }
        public bool ApplicationStateUserTwo { get; set; }
    }
}
