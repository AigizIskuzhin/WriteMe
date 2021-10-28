using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Database.Builder.Infrastructure.Extensions;
using Database.DAL.Context;
using Database.DAL.Entities;
using Database.DAL.Entities.Chat;
using Database.DAL.Entities.Chat.Base;
using Database.DAL.Entities.Chat.GroupChat;
using Microsoft.EntityFrameworkCore;

namespace Database.Builder
{
    class WriteMeDatabaseTestInitializer
    {
        private readonly WriteMeDatabase Database;
        private readonly ILogger<WriteMeDatabaseTestInitializer> Logger;
        public WriteMeDatabaseTestInitializer(WriteMeDatabase database, ILogger<WriteMeDatabaseTestInitializer> logger)
        {
            Database = database;
            Logger = logger;
        }

        public void Initialize()
        {
            Database.Database.EnsureDeleted();
            Database.Database.Migrate();
        }
        public async Task InitializeAsync()
        {
            await Database.Database.EnsureDeletedAsync().ConfigureAwait(false);
            await Database.Database.MigrateAsync();
            return;
            if (!await Database.Roles.AnyAsync())
                await InitializeRoles();
            if (!await Database.Users.AnyAsync())
                await InitializeUsers();
            //if (!await Database.Chats.AnyAsync())
            //{
            //    await InitializeUserToUserChats();
            //    await InitializeGroupChats();
            //}

            //if (!await Database.ChatMessages.AnyAsync())
                //await InitializeMessages();
        }

        #region Инициализация ролей
        /// <summary>
        /// Инициализация ролей доступа пользователей
        /// </summary>
        private async Task InitializeRoles()
        {
            await Database.Roles.AddRangeAsync(new Role { Name = "user" }, new Role { Name = "mod" });
            await Database.SaveChangesAsync();
        }
        #endregion

        #region Инициализация 10-ти пользователей

        private int _UsersCount = 10;
        private User[] _Users;
        /// <summary>
        /// Инициализация 10-ти пользователей
        /// </summary>
        private async Task InitializeUsers()
        {
            _Users = new User[_UsersCount];
            while (_UsersCount > 0)
            {
                _Users[_UsersCount-1] = new User
                {
                    Name = "Иван " + _UsersCount,
                    Surname = "Иванов " + _UsersCount,
                    Patronymic = "Иванович " + _UsersCount,
                    MailAddress = "test" + _UsersCount + "@mail.ru",
                    RoleId=1
                };
                _UsersCount--;
            }
            await Database.Users.AddRangeAsync(_Users);
            await Database.SaveChangesAsync();
        } 
        #endregion

        #region Инициализация 10-ти личных чатов

        private int _ChatsCount = 7;
        private Chat[] _Chats;
        /// <summary>
        /// Инициализация 5-ти личных чатов
        /// </summary>
        private async Task InitializeUserToUserChats()
        {
            Random rnd = new();
            _Chats = new Chat[_ChatsCount];
            while (_ChatsCount > 0)
            {
                //_Chats[_ChatsCount] = new Chat
                //{
                //    IsGroupChat = false,
                //    Participants = new List<ChatParticipant>
                //    {
                //        new() {UserId = rnd.NextItem(_Users).Id},
                //        new() {UserId = rnd.NextItem(_Users).Id}
                //    }
                //};
                _ChatsCount--;
            }
            //await Database.Chats.AddRangeAsync(_Chats);
            await Database.SaveChangesAsync();
        } 
        #endregion
        #region Инициализация 2-х групповых чата
        private Chat[] _GroupChats;
        /// <summary>
        /// Инициализация 2-х групповых чата
        /// </summary>
        private async Task InitializeGroupChats()
        {
            _GroupChats = new Chat[_ChatsCount];
            List<GroupChatParticipant> participants = new List<GroupChatParticipant>();
            //if(_ChatsCount==2)
            //    for (int i = 0; i < _Users.Length/2; i++)
            //        participants.Add(new (){UserId = _Users[i].Id});
            //else
            //    for (int i = _Users.Length/2; i >= _Users.Length/2; i--)
            //        participants.Add(new(){UserId=_Users[i].Id});

            while (_ChatsCount > 0)
            {
                //_GroupChats[_ChatsCount-1] = new Chat
                //{
                //    Id = _ChatsCount,
                //    IsGroupChat = true,
                //    Participants = participants
                //};
                _ChatsCount--;
            }
            //await Database.Chats.AddRangeAsync(_Chats);
            await Database.SaveChangesAsync();
        } 
        #endregion
        #region Инициализация пользователей

        private int _MessagesCount = 100;
        private Message[] _Messages;
        /// <summary>
        /// Инициализация 100 сообщений
        /// </summary>
        private async Task InitializeMessages()
        {
            Random rnd = new ();
            int userId = rnd.NextItem(_Users).Id;
            //int groupChatId;
            //for (int i = 0; i < _GroupChats.Length; i++)
            //    if (_GroupChats[i].Participants.Any(participant => participant.UserId.Equals(userId)))
            //            groupChatId = i + 1;
            
            _Messages = new Message[_MessagesCount];
            while (_MessagesCount > 0)
            {
                //_Messages[_UsersCount] = new GroupChatUserMessage
                //{
                //    SenderId = userId,
                //    Text = "Message "+_MessagesCount,
                //    ChatId = rnd.NextItem(_Chats).Id
                //};
                _MessagesCount--;
            }
            await Database.Users.AddRangeAsync(_Users);
            await Database.SaveChangesAsync();
        } 
        #endregion
    }
}
