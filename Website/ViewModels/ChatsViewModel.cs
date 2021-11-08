using Database.DAL.Entities.Chats.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Website.ViewModels.Messenger;

namespace Website.ViewModels
{
    public class ChatsViewModel
    {
        public IEnumerable<Chat> Chats { get; set; }
        public IEnumerable<Chat> Peers { get; set; } = Enumerable.Empty<Chat>();
        [FromQuery(Name = "id")]
        public int ChatId { get; set; }
        public ChatViewModel ActiveChat { get; set; }
    }
}
