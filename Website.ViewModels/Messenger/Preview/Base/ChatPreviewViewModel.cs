using System;
using System.Linq;
using Website.ViewModels.Base;
using Website.ViewModels.Users;

namespace Website.ViewModels.Messenger.Preview.Base
{
    public class ChatPreviewViewModel
    {
        public ChatViewModel Chat { get; set; }
        public MessageViewModel LastMessage => Chat.History.LastOrDefault();
    }

    public class MessageViewModel : EntityViewModel
    {
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }

    public class UserMessageViewModel : MessageViewModel
    {
        public ChatParticipantViewModel ChatParticipantSender { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }

    public class ChatParticipantViewModel
    {
        public UserViewModel User { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
