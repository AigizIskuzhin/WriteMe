using System;
using Database.DAL.Entities.Base;

namespace Database.DAL.Entities
{
    public class Post : Entity
    {
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
