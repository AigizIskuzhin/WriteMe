using System;
using Database.DAL.Entities.Base;

namespace Database.DAL.Entities.Chat.Base
{
    public abstract class Participant : Entity
    {
        public User User { get; set; }
        public DateTime JoinedDateTime { get; set; }
    }
}