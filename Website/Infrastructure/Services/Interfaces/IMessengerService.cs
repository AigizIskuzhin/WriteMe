using Database.DAL.Entities.Chat.Base;
using System.Collections.Generic;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IMessengerService
    {
        public IEnumerable<Chat> GetUserChats(int id);
        public IEnumerable<Message> GetPrivateChatHistory(int id);
        public Chat GetPrivateChatWithUser(int receiverId, int senderId);
        public Chat GetNewChatWithUser(int receiverUserId, int senderUserId);
        public Message SendMessageToPrivateChat(int privateChatId);
    }
}
