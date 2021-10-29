using System;
using Database.DAL.Entities.Base;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;

namespace Database.DAL.Entities.Chat.Base
{
    public abstract class Participant : Entity, ICreateTimeStampOfEntity
    {
        public User User { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}