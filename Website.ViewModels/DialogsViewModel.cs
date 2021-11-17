using Database.DAL.Entities.Chats.Base;
using System.Collections.Generic;

namespace Website.ViewModels
{
    public class DialogsViewModel
    {
        public IEnumerable<Chat> Chats { get; set; }
        public string Peers { get; set; }
        public IEnumerable<Chat> PeersChats { get; set; }
        public int TargetedUserId { get; set; }
        public int TargetedGroupChatId { get; set; }
    }
}
