using System.Linq;
using Database.DAL.Context;
using Database.DAL.Entities;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Repositories
{
    class ChatsRepository : DbRepository<Chat>
    {
        public ChatsRepository(WriteMeDatabase db) : base(db) {}

        public override IQueryable<Chat> Items => base.Items
            .Include(chat => chat.ChatParticipants)
            .ThenInclude(participant => participant.User)
            .Include(chat => chat.GeneratedChatMessages)
            .Include(chat => chat.ParticipantChatMessages)
            .ThenInclude(participant => participant.ChatParticipantSender)
            .ThenInclude(participant => participant.User);
    }
}
