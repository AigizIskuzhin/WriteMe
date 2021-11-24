using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Website.ViewModels.Messenger;
using Website.ViewModels.Messenger.Preview.Base;

namespace Services.Interfaces
{
    public interface IMessengerService
    {
        public event EventHandler<EventArgs<int>> NewMessageOnChat;
        public IEnumerable<ChatPreviewViewModel> GetUserChatsPreviews(int id);
        public IEnumerable<MessageViewModel> GetPrivateChatHistory(int id);
        public ChatViewModel GetChat(int id, int userId);
        public ChatViewModel GetPrivateChatWithUser(int receiverId, int senderId);
        public ChatViewModel GetNewChatWithUser(int receiverUserId, int senderUserId);
        public Task SendMessageToChat(int userId, int chatId, string text);
        public IEnumerable<MessageViewModel> GetNewMessagesFromLast(int chatId, int lastMessageId);
        public IEnumerable<string> GetChatParticipantIds(int chatId);
    }
}
