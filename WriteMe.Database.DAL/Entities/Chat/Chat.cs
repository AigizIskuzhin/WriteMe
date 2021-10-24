using System;
using System.Collections.Generic;
using WriteMe.Database.DAL.Entities.Base;

namespace WriteMe.Database.DAL.Entities.Chat
{
    public class Chat : Entity
    {
        public virtual ICollection<ChatMessage> History { get; set; }

        public Chat()
        {
            History = new HashSet<ChatMessage>();
        }
        public DateTime ChangeDateTime { get; set; }
        public DateTime CreationDateTime { get; set; }
    }

    public class ChatDialog : Chat
    {
        public int UserOneId { get; set; }
        public User UserOne { get; set; }
        public int UserTwoId { get; set; }
        public User UserTwo { get; set; }
    }

    public class GroupChat : Chat
    {
        public string Title { get; set; }
        public virtual ICollection<ChatParticipant> Participants { get; set; }

        public GroupChat()
        { 
            Participants = new HashSet<ChatParticipant>();;
        }
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
