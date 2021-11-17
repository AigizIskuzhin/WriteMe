using System;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website.ViewModels.Messenger.Preview.Base;

namespace Services.Interfaces
{
    public interface IMessengerService
    {
        public event EventHandler<EventArgs<int>> NewMessageOnChat;
        public IEnumerable<ChatPreviewViewModel> GetUserChatsPreviews(int id);
        public IEnumerable<IMessage> GetPrivateChatHistory(int id);
        public Chat GetChat(int id);
        public Chat GetPrivateChatWithUser(int receiverId, int senderId);
        public Chat GetNewChatWithUser(int receiverUserId, int senderUserId);
        public Task SendMessageToChat(int userId, int chatId, string text);
        public IEnumerable<IMessage> GetNewMessagesFromLast(int chatId, int lastMessageId);
        public IEnumerable<string> GetChatParticipantIds(int chatId);
    }
}
