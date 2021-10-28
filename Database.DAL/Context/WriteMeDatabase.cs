using System;
using Database.DAL.Entities;
using Database.DAL.Entities.Chat;
using Database.DAL.Entities.Chat.Base;
using Database.DAL.Entities.Chat.GroupChat;
using Database.DAL.Entities.Chat.PrivateChat;
using Microsoft.EntityFrameworkCore;

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
        /// Private generated message
        /// </summary>
        public DbSet<PrivateChatMessage> PrivateChatMessages { get; set; }

        /// <summary>
        /// Private User-To-User message
        /// </summary>
        //public DbSet<PrivateChatUserMessage> PrivateChatUserMessages { get; set; }
        #endregion

        #endregion



        public DbSet<FriendshipApplication> FriendshipApplications { get; set; }
        public DbSet<FriendshipType> FriendshipTypes { get; set; }

        public WriteMeDatabase(DbContextOptions<WriteMeDatabase> options) : base(options)
        {

        }
        private DateTime Current_TimeStamp => DateTime.Now;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .Property(post => post.CreationDateTime)
                .HasDefaultValue(Current_TimeStamp);
            modelBuilder.Entity<User>()
                .Property(user => user.RegistrationDateTime)
                .HasDefaultValue(Current_TimeStamp);
            modelBuilder.Entity<PrivateChat>()
                .Property(privateChat => privateChat.CreationDateTime)
                .HasDefaultValue(Current_TimeStamp);
            modelBuilder.Entity<PrivateChat>()
                .Property(privateChat => privateChat.ChangeDateTime)
                .HasDefaultValue(Current_TimeStamp);

            
            modelBuilder.Entity<GroupChat>()
                .Property(privateChat => privateChat.CreationDateTime)
                .HasDefaultValue(Current_TimeStamp);
            modelBuilder.Entity<GroupChat>()
                .Property(privateChat => privateChat.ChangeDateTime)
                .HasDefaultValue(Current_TimeStamp);

            
            modelBuilder.Entity<Message>()
                .Property(privateChat => privateChat.SentDateTime)
                .HasDefaultValue(Current_TimeStamp);

            base.OnModelCreating(modelBuilder);
        }
    }
}
