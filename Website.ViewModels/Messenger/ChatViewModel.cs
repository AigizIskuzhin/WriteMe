using System.Collections.Generic;
using System.Linq;
using Website.ViewModels.Messenger.Preview.Base;
using Website.ViewModels.Users;

namespace Website.ViewModels.Messenger
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public IEnumerable<MessageViewModel> History { get; set; }
        public UserViewModel Receiver { get; set; }
        public MessageViewModel LastMessage => History.LastOrDefault();
        public bool IsReceiverOnline { get; set; }
        public int ConnectedUserId { get; set; }
        public bool IsPrivateChat { get; set; }
    }
}
