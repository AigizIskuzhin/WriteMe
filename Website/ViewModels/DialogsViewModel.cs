using System.Collections.Generic;
using Database.DAL.Entities.Chat;

namespace Website.ViewModels
{
    public class DialogsViewModel
    {
        public IEnumerable<Chat> Chats { get; set; }
    }
}
