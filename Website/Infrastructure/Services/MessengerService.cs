using System;
using System.Collections.Generic;
using System.Linq;
using Database.DAL.Entities;
using Database.DAL.Entities.Chat;
using Database.DAL.Entities.Chat.Base;
using Database.DAL.Entities.Chat.GroupChat;
using Database.DAL.Entities.Chat.PrivateChat;
using Database.Interfaces;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public class MessengerService : IMessengerService
    {
        private readonly IRepository<PrivateChat> PrivateChatsRepository;
        private readonly IRepository<GroupChat> GroupChatsRepository;
        private readonly IRepository<User> UsersRepository;
        public MessengerService(IRepository<User> usersRepository, IRepository<PrivateChat> privateChatsRepository, IRepository<GroupChat> groupChatsRepository)
        {
            UsersRepository = usersRepository;
            PrivateChatsRepository = privateChatsRepository;
            GroupChatsRepository = groupChatsRepository;
        }

        public IEnumerable<Chat> GetUserChats(int id)
        {
            foreach (var privateChat in PrivateChatsRepository.Items.Where(chat => chat.Id.Equals(id)))
                yield return privateChat;
            foreach (var groupChat in GroupChatsRepository.Items.Where(groupChat => groupChat.Id.Equals(id)))
                yield return groupChat;
        }

        public IEnumerable<Message> GetPrivateChatHistory(int id)
        {
            var chat = PrivateChatsRepository.Get(id);
            if(chat is null)
                return Enumerable.Empty<PrivateChatMessage>();
            return chat.History;
        }

        public Chat GetPrivateChatWithUser(int receiverId, int senderId)
        {
            var chat = PrivateChatsRepository.Items.FirstOrDefault(privateChat=>
                privateChat.UserOne.Id.Equals(receiverId) && privateChat.UserTwo.Id.Equals(senderId)||
                privateChat.UserOne.Id.Equals(senderId) && privateChat.UserTwo.Id.Equals(receiverId));
            return chat;
        }

        public Chat GetNewChatWithUser(int receiverUserId, int senderUserId)
        {
            var receiver = UsersRepository.Get(receiverUserId);
            var sender = UsersRepository.Get(senderUserId);
            if (sender is null || receiver is null)
                return null;

            var chat = PrivateChatsRepository.Add(new()
            {
                UserOne = sender,
                UserTwo = receiver
            });

            return chat;
        }
    }
}
