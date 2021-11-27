using Database.DAL.Context;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Database.DAL.Repositories
{
    class ChatsRepository : DbRepository<Chat>
    {
        public ChatsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<Chat> Items => base.Items
            .Include(chat => chat.ChatParticipants)
            .ThenInclude(participant => participant.User)
            .ThenInclude(c => c.Country)
            .Include(chat => chat.ChatParticipants)
            .ThenInclude(participant => participant.User)
            .ThenInclude(c => c.Role)
            .Include(chat => chat.GeneratedChatMessages)
            .Include(chat => chat.ParticipantChatMessages)
            .ThenInclude(participant => participant.ChatParticipantSender)
            .ThenInclude(participant => participant.User)
            .ThenInclude(u => u.Country);
    }
}
