using System.Linq;
using Microsoft.EntityFrameworkCore;
using WriteMe.Database.DAL.Context;
using WriteMe.Database.DAL.Entities.Chat;
using WriteMe.Database.DAL.Repositories.Base;

namespace WriteMe.Database.DAL.Repositories
{
    class ChatsParticipantsRepository : DbRepository<ChatParticipant>
    {
        public ChatsParticipantsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<ChatParticipant> Items => base.Items
            .Include(chatParticipant => chatParticipant.User);
    }
}
