using System;
using Database.DAL.Entities.Chat.Base;

namespace Database.DAL.Entities.Chat.PrivateChat
{
    public class PrivateChatMessage : Message
    {
        public PrivateChat PrivateChat{ get; set; }
    }
}