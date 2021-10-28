using System.Collections.Generic;

namespace Database.DAL.Entities.Chat.GroupChat
{
    public class GroupChat : Base.Chat
    {
        public virtual ICollection<GroupChatParticipant> Participants { get; set; }

        public GroupChat()
        {
            Participants = new HashSet<GroupChatParticipant>();
        }
    }
}