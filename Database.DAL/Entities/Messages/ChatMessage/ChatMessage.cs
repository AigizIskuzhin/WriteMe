using Database.DAL.Entities.Base;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Database.DAL.Entities.Messages.ChatMessage
{
    public class ParticipantChatMessage : Entity, IUserMessage
    {
        public virtual ChatParticipant ChatParticipantSender { get; set; }
        public virtual Chat Chat { get; set; }
        public string Text { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
        [Required]
        public DateTime UpdatedDateTime { get; set; }
    }

    public class GeneratedChatMessage : Entity, IMessage
    {
        public virtual Chat Chat { get; set; }
        public string Text { get; set; }
        
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
