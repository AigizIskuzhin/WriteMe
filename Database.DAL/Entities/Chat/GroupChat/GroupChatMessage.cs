using System;
using Database.DAL.Entities.Chat.Base;

namespace Database.DAL.Entities.Chat.GroupChat
{
    public class GroupChatMessage : Message
    {
        public GroupChat GroupChat;
    }
}