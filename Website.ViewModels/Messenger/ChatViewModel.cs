using System.Collections.Generic;
using System.Linq;
using Website.ViewModels.Messenger.Preview.Base;

namespace Website.ViewModels.Messenger
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public IEnumerable<MessageViewModel> History { get; set; }
        public bool IsReceiverOnline { get; set; }
        public int ConnectedUserId { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public MessageViewModel LastMessage => History.LastOrDefault();
        public bool IsPrivateChat { get; set; }
        public string ReceiverAvatarPath{get;set;}
    }
}
