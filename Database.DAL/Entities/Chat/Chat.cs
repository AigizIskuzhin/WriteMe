using System;
using System.Collections.Generic;
using Database.DAL.Entities.Base;

namespace Database.DAL.Entities.Chat
{
    public class Chat : Entity
    {
        public string Title { get; set; }
        public virtual ICollection<ChatParticipant> Participants { get; set; }
        public virtual ICollection<ChatMessage> History { get; set; }
        public bool IsGroupChat { get; set; }

        public Chat()
        {
            Participants = new HashSet<ChatParticipant>();;
            History = new HashSet<ChatMessage>();
        }
        public DateTime ChangeDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
    public abstract class Participant : Entity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime JoinedDateTime { get; set; }
    }

    public class ChatParticipant : Participant
    {
        public int ChatId { get; set; }
    }

    public abstract class Message : Entity
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public ChatParticipant Sender { get; set; }
    }
    public abstract class ChatMessage : Message
    {
        public int ChatId { get; set; }
    }

    public class UserMessage : ChatMessage
    {
        public int SenderId { get; set; }
        public User Sender { get; set; }
    }

    public class GeneratedMessage : ChatMessage
    {

    }
}
