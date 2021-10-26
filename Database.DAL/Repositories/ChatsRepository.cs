using System.Linq;
using Database.DAL.Context;
using Database.DAL.Entities.Chat;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Repositories
{
    class ChatsRepository : DbRepository<Chat>
    {
        public ChatsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<Chat> Items => base.Items
            .Include(chat => chat.History)
            .Include(chat => chat.Participants)
            .ThenInclude(participant => participant.User);
    }
    //class ChatDialogsRepository : DbRepository<ChatDialog>
    //{
    //    public ChatDialogsRepository(WriteMeDatabase db) : base(db) {}

    //    public override IQueryable<ChatDialog> Items => base.Items
    //        .Include(chatDialog => chatDialog.UserOne)
    //        .Include(chatDialog => chatDialog.UserTwo);
    //}
    //class GroupChatsRepository : DbRepository<GroupChat>
    //{
    //    public GroupChatsRepository(WriteMeDatabase db) : base(db) {}

    //    public override IQueryable<GroupChat> Items => base.Items
    //        .Include(groupChat => groupChat.Participants)
    //        .ThenInclude(participant => participant.User);
    //}
}
