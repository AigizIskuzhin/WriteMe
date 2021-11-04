using Database.DAL.Context;
using Database.DAL.Entities.Messages.ChatMessage;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.DAL.Repositories
{
    class ChatParticipantRepository : DbRepository<ChatParticipant>
    {
        public ChatParticipantRepository(WriteMeDatabase db) : base(db) { }

        public override IQueryable<ChatParticipant> Items => base.Items
            .Include(c => c.User)
            .Include(c=>c.Chat)
            .Include(c=>c.ChatParticipantMessages);
    }
}
