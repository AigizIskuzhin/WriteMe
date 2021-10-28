using Database.DAL.Entities.Base;
using System;

namespace Database.DAL.Entities.Chat.Base
{
    public abstract class Message : Entity
    {
        public string Text { get; set; }
        public DateTime SentDateTime { get; set; }
    }
}