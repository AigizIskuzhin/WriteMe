using Database.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;

namespace Database.DAL.Entities.Chat.Base
{
    public abstract class Chat : Entity, ICreateUpdateTimeStampedEntity
    {
        public virtual ICollection<Message> History { get; set; }

        protected Chat()
        {
            History = new HashSet<Message>();
        }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
