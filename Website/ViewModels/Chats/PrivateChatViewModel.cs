using Database.DAL.Entities;
using Database.DAL.Entities.Chat.Base;
using Database.DAL.Entities.Chat.PrivateChat;
using System.Linq;

namespace Website.ViewModels.Chats
{
    public class PrivateChatViewModel
    {
        public PrivateChat PrivateChat { get; set; }
        private User Receiver { get; }
        public int ConnectedUserId { get; set; }
        public int ReceiverId { get; }
        public string ReceiverName { get; }
        public Message LastMessage { get; }
        public PrivateChatViewModel()
        {
            Receiver = PrivateChat.UserOne.Id == ConnectedUserId ? PrivateChat.UserTwo : PrivateChat.UserOne;
            ReceiverId = Receiver.Id;
            ReceiverName = Receiver.Name;
            LastMessage = PrivateChat.History.OrderByDescending(m => m.CreatedDateTime).FirstOrDefault();
        }
    }
}
