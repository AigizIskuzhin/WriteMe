using Database.DAL.Entities.Base;
using System;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;

namespace Database.DAL.Entities.Chat.Base
{
    public abstract class Message : Entity, ICreateUpdateTimeStampedEntity
    {
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}