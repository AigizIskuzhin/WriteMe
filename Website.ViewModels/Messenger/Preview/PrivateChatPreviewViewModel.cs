using Website.ViewModels.Messenger.Preview.Base;
using Website.ViewModels.Users;

namespace Website.ViewModels.Messenger.Preview
{
    public class PrivateChatPreviewViewModel : ChatPreviewViewModel
    {
        public UserViewModel Receiver { get; set; }
    }
}