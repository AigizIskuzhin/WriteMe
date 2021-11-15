using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;
using Database.Interfaces;

namespace Database.DAL.Entities.Chats.Base
{
    public interface IParticipant : IEntity, ICreateTimeStampOfEntity
    {
        public User User { get; set; }
    }
}