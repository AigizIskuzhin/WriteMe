using System;
using Database.DAL.Entities.Base;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;

namespace Database.DAL.Entities
{
    public class Post : Entity, ICreateUpdateTimeStampedEntity
    {
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
