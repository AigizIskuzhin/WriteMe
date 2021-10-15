using WriteMe.Database.DAL.Entities;

namespace Website.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public bool IsOwner { get; set; }
        public bool IsMod { get; set; }
    }
}
