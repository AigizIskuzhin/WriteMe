using System.Collections.Generic;
using Database.DAL.Entities.Chat;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface IMessengerService
    {
        public IEnumerable<Chat> GetUserChats(int id);
        public IEnumerable<ChatMessage> GetChatHistory(int id);
        public Chat GetChat(int id);
    }
}
