using Database.DAL.Entities.Base;
using System;
using System.Collections.Generic;

namespace Database.DAL.Entities.Chat.Base
{
    public abstract class Chat : Entity
    {
        public string Title { get; set; }
        public virtual ICollection<Message> History { get; set; }

        protected Chat()
        {
            History = new HashSet<Message>();
        }
        public DateTime ChangeDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
