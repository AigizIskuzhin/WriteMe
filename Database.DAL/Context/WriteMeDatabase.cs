﻿using Database.DAL.Entities;
using Database.DAL.Entities.Chat;
using Microsoft.EntityFrameworkCore;

namespace Database.DAL.Context
{
    public class WriteMeDatabase : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; } 
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<GeneratedMessage> GeneratedMessages { get; set; }
        public DbSet<FriendshipApplication> FriendshipApplications { get; set; }
        public DbSet<FriendshipType> FriendshipTypes { get; set; }
        public WriteMeDatabase(DbContextOptions<WriteMeDatabase> options) : base(options){}

        //private const string GetDate = "GETDATE()";
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Post>()
        //        .Property(post => post.CreationDateTime)
        //        .HasDefaultValueSql(GetDate);
        //    modelBuilder.Entity<User>()
        //        .Property(user => user.RegistrationDateTime)
        //        .HasDefaultValueSql(GetDate);

        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
