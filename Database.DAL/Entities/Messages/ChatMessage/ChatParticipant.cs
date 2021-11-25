using Database.DAL.Entities.Base;
using Database.DAL.Entities.Chats.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.DAL.Entities.Messages.ChatMessage
{
    public class ChatParticipant : Entity, IParticipant
    {
        public Chat Chat { get; set; }
        public User User { get; set; }
        public ICollection<ParticipantChatMessage> ChatParticipantMessages { get; set; }
        
        [Required]
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LeftDateTime { get; set; } = null;
        //public ChatParticipant()
        //{
        //    ChatParticipantMessages = new HashSet<ParticipantChatMessage>();
        //}

    }
}