using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;
using System.Collections.Generic;
using Website.ViewModels.Messenger;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IMessengerService
    {
        public IEnumerable<Chat> GetUserChats(int id);
        public IEnumerable<IMessage> GetPrivateChatHistory(int id);
        public Chat GetChat(int id);
        public Chat GetPrivateChatWithUser(int receiverId, int senderId);
        public Chat GetNewChatWithUser(int receiverUserId, int senderUserId);
        public void SendMessageToChat(int userId, int chatId, string text);
        public IEnumerable<IMessage> GetNewMessagesFromLast(int chatId, int lastMessageId);
    }
}
