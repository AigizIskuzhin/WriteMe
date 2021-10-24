﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WriteMe.Database.DAL.Context;

namespace WriteMe.Database.DAL.Migrations
{
    [DbContext(typeof(WriteMeDatabase))]
    partial class WriteMeDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

<<<<<<< HEAD
            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.Chat", b =>
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

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.ChatMessage", b =>
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

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.ChatParticipant", b =>
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

=======
>>>>>>> dev
            modelBuilder.Entity("WriteMe.Database.DAL.Entities.FriendshipApplication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("ApplicationStateUserOne")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ApplicationStateUserTwo")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserOneId")
                        .HasColumnType("int");

                    b.Property<int>("UserTwoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserOneId");

                    b.HasIndex("UserTwoId");

                    b.ToTable("FriendshipApplications");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Post", b =>
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

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Role", b =>
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

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.User", b =>
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

                    b.Property<DateTime>("RegistrationDateTime")
                        .HasColumnType("datetime(6)");

<<<<<<< HEAD
                    b.Property<int>("RoleId")
=======
                    b.Property<int?>("RoleId")
>>>>>>> dev
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.UserConnection", b =>
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

<<<<<<< HEAD
            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.GeneratedMessage", b =>
                {
                    b.HasBaseType("WriteMe.Database.DAL.Entities.Chat.ChatMessage");

                    b.HasDiscriminator().HasValue("GeneratedMessage");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.UserMessage", b =>
                {
                    b.HasBaseType("WriteMe.Database.DAL.Entities.Chat.ChatMessage");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasIndex("SenderId");

                    b.HasDiscriminator().HasValue("UserMessage");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.ChatMessage", b =>
                {
                    b.HasOne("WriteMe.Database.DAL.Entities.Chat.Chat", null)
                        .WithMany("History")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.ChatParticipant", b =>
                {
                    b.HasOne("WriteMe.Database.DAL.Entities.Chat.Chat", null)
                        .WithMany("Participants")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WriteMe.Database.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

=======
>>>>>>> dev
            modelBuilder.Entity("WriteMe.Database.DAL.Entities.FriendshipApplication", b =>
                {
                    b.HasOne("WriteMe.Database.DAL.Entities.User", "UserOne")
                        .WithMany()
                        .HasForeignKey("UserOneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WriteMe.Database.DAL.Entities.User", "UserTwo")
                        .WithMany()
                        .HasForeignKey("UserTwoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserOne");

                    b.Navigation("UserTwo");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Post", b =>
                {
                    b.HasOne("WriteMe.Database.DAL.Entities.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.User", b =>
                {
                    b.HasOne("WriteMe.Database.DAL.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.UserConnection", b =>
                {
                    b.HasOne("WriteMe.Database.DAL.Entities.User", null)
                        .WithMany("ConnectionIdentifiers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.UserMessage", b =>
                {
                    b.HasOne("WriteMe.Database.DAL.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Chat.Chat", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WriteMe.Database.DAL.Entities.User", b =>
                {
                    b.Navigation("ConnectionIdentifiers");
                });
#pragma warning restore 612, 618
        }
    }
}
