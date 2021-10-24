using System.Linq;
using Microsoft.EntityFrameworkCore;
using WriteMe.Database.DAL.Context;
using WriteMe.Database.DAL.Entities.Chat;
using WriteMe.Database.DAL.Repositories.Base;

namespace WriteMe.Database.DAL.Repositories
{
    class ChatsRepository : DbRepository<Chat>
    {
        public ChatsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<Chat> Items => base.Items
            .Include(post => post.History);
    }
    class ChatDialogsRepository : DbRepository<ChatDialog>
    {
        public ChatDialogsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<ChatDialog> Items => base.Items
            .Include(chatDialog => chatDialog.UserOne)
            .Include(chatDialog => chatDialog.UserTwo);
    }
    class GroupChatsRepository : DbRepository<GroupChat>
    {
        public GroupChatsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<GroupChat> Items => base.Items
            .Include(groupChat => groupChat.Participants)
            .ThenInclude(participant => participant.User);
    }
}
