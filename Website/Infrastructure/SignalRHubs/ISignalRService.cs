using System;
using System.Threading.Tasks;

namespace Website.Infrastructure.SignalRHubs
{
    public interface ISignalRService
    {
        public event EventHandler<EventArgs<string>> UserJoin;
        
        public event EventHandler<EventArgs<string>> UserLeft;
        public event EventHandler<EventArgs<PrivateNotificationMessage>> NewMessage;

        public ConnectionMapping<string> Connections { get; }
        public void UserJoining(string id, string connectionId);
        public void UserLeaving(string id, string connectionId);
        public Task NotifyUserFromPrivateChatAboutNewMessage(string chatId, string senderId);
    }

    public class PrivateNotificationMessage
    {
        public PrivateNotificationMessage(string chatId, string receiverId)
        {
            ChatId = chatId;
            ReceiverId = receiverId;
        }

        public string ChatId { get; }
        public string ReceiverId { get; }
    }
}
