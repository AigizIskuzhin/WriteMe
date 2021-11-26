using Database.DAL.Entities;
using Database.DAL.Entities.Base;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.ChatMessage;
using Database.DAL.Extensions.MySqlTimeStamps;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Context
{
    public class WriteMeDatabase : DbContext
    {
        #region Users
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Role> Roles { get; set; } 
        #endregion

        #region Posts
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<SystemPost> SystemPosts { get; set; } 
        #endregion

        #region Reports
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<ReportState> ReportStates { get; set; }
        public DbSet<PostReport> PostReports { get; set; } 
        #endregion

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

            #region Configuration timestamps for create and update

            builder.Entity<ParticipantChatMessage>().UseBothTimeStampedProperties();
            builder.Entity<Chat>().UseBothTimeStampedProperties();
            builder.Entity<UserPost>().UseBothTimeStampedProperties();
            builder.Entity<SystemPost>().UseBothTimeStampedProperties();
            builder.Entity<User>().UseBothTimeStampedProperties();
            builder.Entity<GeneratedChatMessage>().UseCreationTimeStampOnProperty();
            builder.Entity<ChatParticipant>().UseCreationTimeStampOnProperty();
            builder.Entity<PostReport>().UseCreationTimeStampOnProperty();

            #endregion
            
            #region Default data
            builder.Entity<Role>().HasData(
                    new Role
                    {
                        Name = "Пользователь",
                        Code = "user",
                        Id = 1
                    },
                    new Role
                    {
                        Name = "Модератор",
                        Code = "mod",
                        Id = 2
                    },
                    new Role
                    {
                        Name = "Администратор",
                        Code = "admin",
                        Id = 3
                    });

            builder.Entity<Country>().HasData(
                new Country
                {
                    CountryCode = 0,
                    Id = 1,
                    Name = "Другая страна"
                },
                new Country
                {
                    CountryCode = 7,
                    Id = 2,
                    Name = "Россия"

                });

            builder.Entity<ReportType>().HasData(
                new ReportType { Name = "Мат" },
                new ReportType { Name = "Другое" });

            builder.Entity<ReportState>().HasData(
                new ReportState { Name = "В процессе" },
                new ReportState { Name = "Рассмотрено" }); 
            #endregion
        }
    }
}
