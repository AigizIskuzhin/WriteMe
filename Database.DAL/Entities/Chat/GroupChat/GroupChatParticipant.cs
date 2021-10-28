using Database.DAL.Entities.Chat.Base;

namespace Database.DAL.Entities.Chat.GroupChat
{
    public class GroupChatParticipant : Participant
    {
        public GroupChat GroupChat;
    }
}