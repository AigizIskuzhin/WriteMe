﻿// <auto-generated />
using System;
using Database.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.DAL.Migrations
{
    [DbContext(typeof(WriteMeDatabase))]
    [Migration("20211026110625_initial2")]
    partial class initial2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Database.DAL.Entities.Chat.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ChangeDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsGroupChat")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.ChatMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("ChatMessages");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ChatMessage");
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.ChatParticipant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinedDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatParticipant");
                });

            modelBuilder.Entity("Database.DAL.Entities.FriendshipApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("ApplicationStateUserOne")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ApplicationStateUserTwo")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("UserOneFriendshipTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("UserOneId")
                        .HasColumnType("int");

                    b.Property<int?>("UserTwoFriendshipTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("UserTwoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserOneFriendshipTypeId");

                    b.HasIndex("UserOneId");

                    b.HasIndex("UserTwoFriendshipTypeId");

                    b.HasIndex("UserTwoId");

                    b.ToTable("FriendshipApplications");
                });

            modelBuilder.Entity("Database.DAL.Entities.FriendshipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("FriendshipTypes");
                });

            modelBuilder.Entity("Database.DAL.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Database.DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Database.DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Birthday")
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsNew")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MailAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Database.DAL.Entities.UserConnection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ConnectionID")
                        .HasColumnType("longtext");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserConnection");
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.GeneratedMessage", b =>
                {
                    b.HasBaseType("Database.DAL.Entities.Chat.ChatMessage");

                    b.HasDiscriminator().HasValue("GeneratedMessage");
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.UserMessage", b =>
                {
                    b.HasBaseType("Database.DAL.Entities.Chat.ChatMessage");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasIndex("SenderId");

                    b.HasDiscriminator().HasValue("UserMessage");
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.ChatMessage", b =>
                {
                    b.HasOne("Database.DAL.Entities.Chat.Chat", null)
                        .WithMany("History")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.ChatParticipant", b =>
                {
                    b.HasOne("Database.DAL.Entities.Chat.Chat", null)
                        .WithMany("Participants")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Database.DAL.Entities.FriendshipApplication", b =>
                {
                    b.HasOne("Database.DAL.Entities.FriendshipType", "UserOneFriendshipType")
                        .WithMany()
                        .HasForeignKey("UserOneFriendshipTypeId");

                    b.HasOne("Database.DAL.Entities.User", "UserOne")
                        .WithMany()
                        .HasForeignKey("UserOneId");

                    b.HasOne("Database.DAL.Entities.FriendshipType", "UserTwoFriendshipType")
                        .WithMany()
                        .HasForeignKey("UserTwoFriendshipTypeId");

                    b.HasOne("Database.DAL.Entities.User", "UserTwo")
                        .WithMany()
                        .HasForeignKey("UserTwoId");

                    b.Navigation("UserOne");

                    b.Navigation("UserOneFriendshipType");

                    b.Navigation("UserTwo");

                    b.Navigation("UserTwoFriendshipType");
                });

            modelBuilder.Entity("Database.DAL.Entities.Post", b =>
                {
                    b.HasOne("Database.DAL.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Database.DAL.Entities.User", b =>
                {
                    b.HasOne("Database.DAL.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Database.DAL.Entities.UserConnection", b =>
                {
                    b.HasOne("Database.DAL.Entities.User", null)
                        .WithMany("ConnectionIdentifiers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.UserMessage", b =>
                {
                    b.HasOne("Database.DAL.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Database.DAL.Entities.Chat.Chat", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("Database.DAL.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Database.DAL.Entities.User", b =>
                {
                    b.Navigation("ConnectionIdentifiers");
                });
#pragma warning restore 612, 618
        }
    }
}
