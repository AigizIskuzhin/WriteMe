using System;
using Database.DAL.Entities;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;
using Database.DAL.Entities.Messages.ChatMessage;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Website.ViewModels.Messenger;
using Website.ViewModels.Messenger.Preview;
using Website.ViewModels.Messenger.Preview.Base;
using Website.ViewModels.Users;

namespace Services
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

        public event EventHandler<Interfaces.EventArgs<int>> NewMessageOnChat;

        public IEnumerable<ChatPreviewViewModel> GetUserChatsPreviews(int userId)
        {
            var chats = ChatsRepository.Items.Where(chat =>
                chat.ChatParticipants.Any(participant => participant.User.Id.Equals(userId)));
            if(!chats.Any()) yield break;
            foreach (var chat in chats)
                if (chat.IsPrivateChat)
                    yield return new PrivateChatPreviewViewModel
                    {
                        Chat = GetChatViewModel(chat, userId),
                        Receiver = GetUserViewModel(chat.GetReceiverChatParticipant(userId).User)
                    };
                //else
                //    yield return new GroupChatPreviewViewModel
                //    {
                //        Chat = get
                //    };
        }

        public ChatViewModel GetChatViewModel(Chat chat, int userId) => new()
        {
            ConnectedUserId = userId,
            History = from m in chat.GetHistory() select GetMessageViewModel(m)
        };

        public MessageViewModel GetMessageViewModel(IMessage message) =>
            message switch
            {
                ParticipantChatMessage userMessage => new UserMessageViewModel
                {
                    Id = message.Id,
                    ChatParticipantSender = new()
                    {
                        User = GetUserViewModel(userMessage.ChatParticipantSender.User),
                        CreatedDateTime = userMessage.ChatParticipantSender.CreatedDateTime
                    },
                    CreatedDateTime = userMessage.CreatedDateTime,
                    Text = userMessage.Text
                },
                GeneratedChatMessage gMessage => new MessageViewModel
                {
                    Id = message.Id, CreatedDateTime = message.CreatedDateTime, Text = message.Text
                },
                _ => null
            };

        public UserViewModel GetUserViewModel(User u) => new UserViewModel
        {
            Id=u.Id,
            Name = u.Name,
            Surname = u.Surname,
            Patronymic = u.Patronymic,
            Birthday = u.Birthday
        };
        public IEnumerable<IMessage> GetPrivateChatHistory(int id)
        {
            var chat = ChatsRepository.Get(id);
            return chat is null ? Enumerable.Empty<IMessage>() : chat.GetHistory();
        }

        public Chat GetChat(int id) => ChatsRepository.Get(id);

        public Chat GetPrivateChatWithUser(int receiverId, int senderId)
        {
            var chat = ChatsRepository.Items
                .FirstOrDefault(c => c.MaximumChatParticipants == 2 &&
                                     c.ChatParticipants.Any(p => p.User.Id.Equals(receiverId)) &&
                                     c.ChatParticipants.Any(p => p.User.Id.Equals(senderId))) ?? GetNewChatWithUser(receiverId, senderId);

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

            var chatParticipantOne = ChatParticipantsRepository.Items
                                         .FirstOrDefault(p =>
                                             p.User.Id.Equals(receiverUserId) && p.Chat.Id.Equals(chat.Id)) ?? 
                                     ChatParticipantsRepository.Add(new() { Chat = chat, User = receiver });
            
            var chatParticipantTwo = ChatParticipantsRepository.Items
                                         .FirstOrDefault(p =>
                                             p.User.Id.Equals(senderUserId) && p.Chat.Id.Equals(chat.Id)) ?? 
                                     ChatParticipantsRepository.Add(new() { Chat = chat, User = sender });


            chat.ChatParticipants.Add(chatParticipantOne);
            chat.ChatParticipants.Add(chatParticipantTwo);
            ChatsRepository.Update(chat);
            return chat;
        }

        public async Task SendMessageToChat(int userId, int chatId, string text)
        {
            var chatParticipants = ChatParticipantsRepository.Items.Where(p => p.Chat.Id.Equals(chatId));
            var chatParticipant = await chatParticipants.FirstOrDefaultAsync(p => p.User.Id.Equals(userId));
            chatParticipant?.ChatParticipantMessages.Add(new ParticipantChatMessage
            {
                Chat = chatParticipant.Chat,
                ChatParticipantSender = chatParticipant,
                Text = text
            });
            await ChatParticipantsRepository.UpdateAsync(chatParticipant);

            NewMessageOnChat?.Invoke(this, chatId);
            //foreach (var receiver in chatParticipants.Where(p=>p.User.Id!=userId))
            //    foreach (var connection in SignalService.Connections.GetConnections(receiver.User.Id.ToString()))
            //        await AppHub.Clients.Client(connection).SendAsync("NotifyAboutNewMessage", chatId);

        }
        
        public IEnumerable<IMessage> GetNewMessagesFromLast(int chatId, int lastMessageId)
        {
            var chat = ChatsRepository.Get(chatId);
            var lastMessage = chat.GetHistory().FirstOrDefault(message => message.Id.Equals(lastMessageId));
            return lastMessage is null
                ? chat.GetHistory()
                : chat.GetHistory().Where(message => message.CreatedDateTime > lastMessage.CreatedDateTime);
        }

        public IEnumerable<string> GetChatParticipantIds(int chatId)
        {
            foreach (var chatParticipant in ChatParticipantsRepository.Items.Where(p => p.Chat.Id == chatId))
                yield return chatParticipant.User.Id.ToString();
        }
    }
}
