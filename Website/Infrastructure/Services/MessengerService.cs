using Database.DAL.Entities;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;
using Database.DAL.Entities.Messages.ChatMessage;
using Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Website.Infrastructure.Services.Interfaces;
using Website.Infrastructure.SignalRHubs;
using Website.ViewModels.Messenger.Preview;
using Website.ViewModels.Messenger.Preview.Base;

namespace Website.Infrastructure.Services
{
    public class MessengerService : IMessengerService
    {
        private readonly IHubContext<AppHub> AppHub;
        private readonly ISignalRService SignalService;



        private readonly IRepository<User> UsersRepository;
        private readonly IRepository<Chat> ChatsRepository;
        private readonly IRepository<ChatParticipant> ChatParticipantsRepository;
        public MessengerService(
            IRepository<User> usersRepository,
            IRepository<Chat> chatsRepository, 
            IRepository<ChatParticipant> chatParticipantsRepository, 
            IHubContext<AppHub> appHub, 
            ISignalRService signalService)
        {
            UsersRepository = usersRepository;
            ChatsRepository = chatsRepository;
            ChatParticipantsRepository = chatParticipantsRepository;
            AppHub = appHub;
            SignalService = signalService;
        }

        public IEnumerable<ChatPreviewViewModel> GetUserChatsPreviews(int userId)
        {
            var chats = ChatsRepository.Items.Where(chat =>
                chat.ChatParticipants.Any(participant => participant.User.Id.Equals(userId)));
            if(!chats.Any()) yield break;
            foreach (var chat in chats)
                if (chat.IsPrivateChat)
                    yield return new PrivateChatPreviewViewModel
                    {
                        Chat = chat,
                        Receiver = chat.GetReceiverChatParticipant(userId).User
                    };
                else
                    yield return new GroupChatPreviewViewModel
                    {
                        Chat = chat
                    };
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
                .FirstOrDefault(c => c.MaximumChatParticipants == 2 &&
                                     c.ChatParticipants.Any(p => p.User.Id.Equals(receiverId)) &&
                                     c.ChatParticipants.Any(p => p.User.Id.Equals(senderId)));

            if(chat is null) chat = GetNewChatWithUser(receiverId, senderId);

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
            
            foreach (var receiver in chatParticipants.Where(p=>p.User.Id!=userId))
                foreach (var connection in SignalService.Connections.GetConnections(receiver.User.Id.ToString()))
                    await AppHub.Clients.Client(connection).SendAsync("NotifyAboutNewMessage", chatId);
            
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
