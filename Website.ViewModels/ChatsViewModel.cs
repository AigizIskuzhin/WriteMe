using System.Collections.Generic;
using Website.ViewModels.Messenger.Preview.Base;

namespace Website.ViewModels
{
    public class ChatsViewModel
    {
        public IEnumerable<ChatPreviewViewModel> ChatsPreviews { get; set; }
        public int id { get; set; }
    }
}
