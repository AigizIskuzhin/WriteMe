using System.Linq;
using Database.DAL.Context;
using Database.DAL.Entities.Chat;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Repositories
{
    class ChatsParticipantsRepository : DbRepository<ChatParticipant>
    {
        public ChatsParticipantsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<ChatParticipant> Items => base.Items
            .Include(chatParticipant => chatParticipant.User);
    }
}
