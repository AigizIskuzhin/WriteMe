using Database.DAL.Entities.Chat.Base;

namespace Database.DAL.Entities.Chat.PrivateChat
{
    public abstract class PrivateChatMessage : Message
    {
        public PrivateChat PrivateChat{ get; set; }
    }
}