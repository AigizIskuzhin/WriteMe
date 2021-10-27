using System.Collections.Generic;
using System.Linq;
using Database.DAL.Entities.Chat;
using Database.Interfaces;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public class MessengerService : IMessengerService
    {
        private readonly IRepository<Chat> ChatRepository;
        public MessengerService(IRepository<Chat> chatRepository)
        {
            ChatRepository = chatRepository;
        }

        public IEnumerable<Chat> GetUserChats(int id) => ChatRepository.Items
            .Where(chat => chat.Participants.Any(participant => participant.User.Id == id));

        public IEnumerable<ChatMessage> GetChatHistory(int id) => ChatRepository.Get(id).History;
        public Chat GetChat(int id) => ChatRepository.Get(id);
    }
}
