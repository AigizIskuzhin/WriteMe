using System;
using Database.DAL.Entities.Base;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;

namespace Database.DAL.Entities
{
    public abstract class Post : Entity, ICreateUpdateTimeStampedEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
    public class SystemPost: Post{}
    public class UserPost : Post
    {
        public int OwnerId { get; set; }
        public User Owner { get; set; }

    }
}
