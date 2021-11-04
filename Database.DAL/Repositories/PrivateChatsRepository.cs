//using Database.DAL.Context;
//using Database.DAL.Repositories.Base;
//using Microsoft.EntityFrameworkCore;
//using System.Linq;
//using Database.DAL.Entities.Chats.GroupChat;
//using Database.DAL.Entities.Chats.PrivateChat;

//namespace Database.DAL.Repositories
//{
//    class PrivateChatsRepository : DbRepository<PrivateChat>
//    {
//        public PrivateChatsRepository(WriteMeDatabase db) : base(db) { }

//        public override IQueryable<PrivateChat> Items => base.Items
//            .Include(c => c.UserOne)
//            .Include(c => c.UserTwo);
//    }
//    class GroupChatsRepository : DbRepository<GroupChat>
//    {
//        public GroupChatsRepository(WriteMeDatabase db) : base(db) { }

//        public override IQueryable<GroupChat> Items => base.Items
//            .Include(c => c.Participants)
//            .ThenInclude(c => c.User);
//    }
//}
