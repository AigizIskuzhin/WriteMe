using System;
using Database.DAL.Entities;

namespace Website.ViewModels
{
    public class UserPostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }

        public bool IsOwner { get; set; }
        public bool IsMod { get; set; }
    }
}
