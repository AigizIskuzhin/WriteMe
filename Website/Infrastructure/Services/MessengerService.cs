using System;
using System.Collections.Generic;
using System.Linq;
using Database.DAL.Entities;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;
using Database.DAL.Entities.Messages.ChatMessage;
using Database.Interfaces;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.Services
{
    public class MessengerService : IMessengerService
    {
        private readonly IRepository<User> UsersRepository;
        private readonly IRepository<Chat> ChatsRepository;
        private readonly IRepository<ChatParticipant> ChatParticipantsRepository;
        public MessengerService(
            IRepository<User> usersRepository,
            IRepository<Chat> chatsRepository, 
            IRepository<ChatParticipant> chatParticipantsRepository)
        {
            UsersRepository = usersRepository;
            ChatsRepository = chatsRepository;
            ChatParticipantsRepository = chatParticipantsRepository;
        }

        public IEnumerable<Chat> GetUserChats(int userId)
        {
            return ChatsRepository.Items.Where(chat =>
                chat.ChatParticipants.Any(participant => participant.User.Id.Equals(userId)));
        }

        public IEnumerable<IMessage> GetPrivateChatHistory(int id)
        {
            var chat = ChatsRepository.Get(id);
            return chat is null ? Enumerable.Empty<IMessage>() : chat.GetHistory();
        }

        public Chat GetChat(int id) => ChatsRepository.Get(id);

        public Chat GetPrivateChatWithUser(int receiverId, int senderId)
        {
            var chat = ChatsRepository.Items
                           .FirstOrDefault(privateChat=> 
                               privateChat.IsPrivateChat && privateChat.MaximumChatParticipants == 2 && 
                               privateChat.ChatParticipants.Any(participant=>participant.User.Id.Equals(receiverId)) && 
                               privateChat.ChatParticipants.Any(participant=>participant.User.Id.Equals(receiverId))) 
                       ?? GetNewChatWithUser(receiverId, senderId);
            return chat;
        }

        public Chat GetNewChatWithUser(int receiverUserId, int senderUserId)
        {
            var receiver = UsersRepository.Get(receiverUserId);
            var sender = UsersRepository.Get(senderUserId);
            if (sender is null || receiver is null)
                return null;

            var chat = ChatsRepository.Add(new()
            {
                IsPrivateChat = true,
                MaximumChatParticipants = 2,
            });

            var chatParticipantOne =
                ChatParticipantsRepository.Items.FirstOrDefault(participant => participant.User.Equals(receiver)) ??
                ChatParticipantsRepository.Add(new() { Chat = chat, User = receiver });
            
            var chatParticipantTwo =
                ChatParticipantsRepository.Items.FirstOrDefault(participant => participant.User.Equals(sender)) ??
                ChatParticipantsRepository.Add(new() { Chat = chat, User = sender });


            chat.ChatParticipants.Add(chatParticipantOne);
            chat.ChatParticipants.Add(chatParticipantTwo);
            return chat;
        }

        public void SendMessageToChat(int userId, int chatId, string text)
        {
            var chatParticipant =
                ChatParticipantsRepository.Items.FirstOrDefault(participant => participant.User.Id.Equals(userId));
            if (chatParticipant != null)
                chatParticipant.ChatParticipantMessages.Add(new ParticipantChatMessage
                {
                    Chat = chatParticipant.Chat,
                    ChatParticipantSender = chatParticipant,
                    Text = text
                });
            ChatParticipantsRepository.Update(chatParticipant);
        }
        
        public IEnumerable<IMessage> GetNewMessagesFromLast(int chatId, int lastMessageId)
        {
            var chat = ChatsRepository.Get(chatId);
            var lastMessage = chat.GetHistory().FirstOrDefault(message => message.Id.Equals(lastMessageId));
            return lastMessage is null
                ? chat.GetHistory()
                : chat.GetHistory().Where(message => message.CreatedDateTime > lastMessage.CreatedDateTime);
        }
    }
}
