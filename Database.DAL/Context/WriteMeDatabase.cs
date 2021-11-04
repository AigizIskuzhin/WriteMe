using Database.DAL.Entities;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.ChatMessage;
using Database.DAL.Extensions.MySqlTimeStamps;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Context
{
    public class WriteMeDatabase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<UserPost> Posts { get; set; }
        public DbSet<SystemPost> SystemPosts { get; set; }

        #region Chat

        private DbSet<Chat> Chats { get; set; }
        public DbSet<ChatParticipant> ChatParticipants { get; set; }
        public DbSet<ParticipantChatMessage> ParticipantChatMessages { get; set; }
        public DbSet<GeneratedChatMessage> GeneratedChatMessages { get; set; }


        #region PrivateChat
        /// <summary>
        /// User-To-User chats
        /// </summary>
        //public DbSet<PrivateChat> PrivateChats { get; set; }

        /// <summary>
        /// Private generated messages
        /// </summary>
        //public DbSet<PrivateChatMessage> PrivateChatMessages { get; set; }

        /// <summary>
        /// Private User-To-User messages
        /// </summary>
        //public DbSet<PrivateChatUserMessage> PrivateChatUserMessages { get; set; }
        #endregion

        #region GroupChat
        /// <summary>
        /// Group chat generated messages
        /// </summary>
        //public DbSet<GroupChatMessage> GroupChatMessages { get; set; }

        /// <summary>
        /// Group chat participants messages
        /// </summary>
        //public DbSet<GroupChatParticipantMessage> GroupChatParticipantMessages { get; set; }
        #endregion

        #endregion

        public DbSet<FriendshipApplication> FriendshipApplications { get; set; }
        public DbSet<FriendshipType> FriendshipTypes { get; set; }

        public WriteMeDatabase(DbContextOptions<WriteMeDatabase> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ParticipantChatMessage>().UseBothTimeStampedProperties();
            builder.Entity<Chat>().UseBothTimeStampedProperties();
            builder.Entity<UserPost>().UseBothTimeStampedProperties();
            builder.Entity<User>().UseBothTimeStampedProperties();
            builder.Entity<GeneratedChatMessage>().UseCreationTimeStampOnProperty();
            builder.Entity<ChatParticipant>().UseCreationTimeStampOnProperty();
            // Other model creating stuff here ...  
        }
    }
}
