using Database.DAL.Entities;
using Website.ViewModels.Messenger.Preview.Base;

namespace Website.ViewModels.Messenger.Preview
{
    public class PrivateChatPreviewViewModel : ChatPreviewViewModel
    {
        public User Receiver { get; set; }
    }
}