using Microsoft.EntityFrameworkCore;
using System.Linq;
using WriteMe.Database.DAL.Context;
using WriteMe.Database.DAL.Entities;
using WriteMe.Database.DAL.Entities.Chat;
using WriteMe.Database.DAL.Repositories.Base;

namespace WriteMe.Database.DAL
{
    class ChatsParticipantsRepository : DbRepository<ChatParticipant>
    {
        public ChatsParticipantsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<ChatParticipant> Items => base.Items
            .Include(chatParticipant => chatParticipant.User);
    }
}
