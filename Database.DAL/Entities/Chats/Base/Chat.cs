using Database.DAL.Entities.Base;
using Database.DAL.Entities.Messages.Base;
using Database.DAL.Entities.Messages.ChatMessage;
using Database.DAL.Extensions.MySqlTimeStamps.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Database.DAL.Entities.Chats.Base
{
    public class Chat : Entity, ICreateUpdateTimeStampedEntity
    {
        /// <summary>
        /// Chat title for preview
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Chat participants messages
        /// </summary>
        public ICollection<ParticipantChatMessage> ParticipantChatMessages { get; set; }

        private int _MaximumChatParticipants { get; set; }

        /// <summary>
        /// Maximum chat participants
        /// </summary>
        public int MaximumChatParticipants
        {
            get => _MaximumChatParticipants;
            set
            {
                if (IsPrivateChat)
                    value = 2;
                _MaximumChatParticipants = value;
            }
        }

        /// <summary>
        /// If chat is private, then maximum of chat participants is 2
        /// </summary>
        public bool IsPrivateChat { get; set; }
        /// <summary>
        /// Generated chat messages
        /// </summary>
        public ICollection<GeneratedChatMessage> GeneratedChatMessages { get; set; }

        /// <summary>
        /// Returns combined IEnumerable of ParticipantChatMessages and GeneratedChatMessages
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMessage> GetHistory()
        {
            IEnumerable<IMessage> genHistory = GeneratedChatMessages;
            IEnumerable<IMessage> history = genHistory.Concat(ParticipantChatMessages);
            return history.OrderBy(m => m.CreatedDateTime);
        }
        /// <summary>
        /// Chat participants
        /// </summary>
        public virtual ICollection<ChatParticipant> ChatParticipants { get; set; }

        public ChatParticipant GetReceiverChatParticipant(int senderUserId)
        {
            if (!IsPrivateChat) return null;

            //var sender = ChatParticipants.FirstOrDefault(participant => participant.User.Id.Equals(senderUserId));
            //if (sender is null) return null;

            var receiver = ChatParticipants.First(participant => participant.User.Id!=senderUserId);

            return receiver;
        }
        public Chat()
        {
            GeneratedChatMessages = new HashSet<GeneratedChatMessage>();
            ParticipantChatMessages = new HashSet<ParticipantChatMessage>();
            ChatParticipants = new HashSet<ChatParticipant>();
        }
        /// <summary>
        /// Chat creation date time
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
        /// <summary>
        /// Chat change date time
        /// </summary>
        public DateTime UpdatedDateTime { get; set; }
    }
}
