using System.Linq;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;

namespace Website.ViewModels.Messenger.Preview.Base
{
    public class ChatPreviewViewModel
    {
        public Chat Chat { get; set; }
        public IMessage LastMessage => Chat.GetHistory().LastOrDefault();
    }
}
