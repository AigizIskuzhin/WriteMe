using Database.DAL.Entities.Chat.Base;

namespace Database.DAL.Entities.Chat.GroupChat
{
    public class GroupChatParticipantMessage : GroupChatMessage
    {
        public Participant Sender { get; set; }
    }
}