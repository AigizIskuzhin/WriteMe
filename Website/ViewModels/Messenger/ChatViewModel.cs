using Database.DAL.Entities.Messages.Base;
using System.Collections.Generic;
using System.Linq;

namespace Website.ViewModels.Messenger
{
    public class ChatViewModel
    {
        public int Id { get; set; }
        public IEnumerable<IMessage> History { get; set; }
        public bool IsReceiverOnline { get; set; }
        public int ConnectedUserId { get; set; }
        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public IMessage LastMessage => History.Last();
        public bool IsPrivateChat { get; set; }
    }
}
