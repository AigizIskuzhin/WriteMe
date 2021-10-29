using Database.DAL.Entities;
using Database.DAL.Entities.Chat.Base;
using Database.DAL.Entities.Chat.GroupChat;
using Database.DAL.Entities.Chat.PrivateChat;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database.DAL.Context
{
    public class WriteMeDatabase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Post> Posts { get; set; }

        #region Chat

        #region PrivateChat
        /// <summary>
        /// User-To-User chats
        /// </summary>
        public DbSet<PrivateChat> PrivateChats { get; set; }

        /// <summary>
        /// Private generated messages
        /// </summary>
        public DbSet<PrivateChatMessage> PrivateChatMessages { get; set; }

        /// <summary>
        /// Private User-To-User messages
        /// </summary>
        public DbSet<PrivateChatUserMessage> PrivateChatUserMessages { get; set; }
        #endregion

        #region GroupChat
        /// <summary>
        /// Group chat generated messages
        /// </summary>
        public DbSet<GroupChatMessage> GroupChatMessages { get; set; }

        /// <summary>
        /// Group chat participants messages
        /// </summary>
        public DbSet<GroupChatParticipantMessage> GroupChatParticipantMessages { get; set; }
        #endregion

        #endregion

        public DbSet<FriendshipApplication> FriendshipApplications { get; set; }
        public DbSet<FriendshipType> FriendshipTypes { get; set; }

        public WriteMeDatabase(DbContextOptions<WriteMeDatabase> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Other model creating stuff here ...  
        }
    }
}
