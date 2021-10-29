using Database.DAL.Entities.Chat.Base;
using System.Collections.Generic;

namespace Website.ViewModels
{
    public class ChatsViewModel
    {
        public IEnumerable<Chat> Chats { get; set; }
        public string Peers { get; set; }
        public IEnumerable<Chat> PeersChats { get; set; }
        public int TargetedUserId { get; set; }
        public int TargetedGroupChatId { get; set; }
        public int ActiveChatId { get; set; }
    }
}
