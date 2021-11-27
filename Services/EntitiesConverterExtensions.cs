using System;
using System.Linq;
using Database.DAL.Entities;
using Database.DAL.Entities.Chats.Base;
using Database.DAL.Entities.Messages.Base;
using Database.DAL.Entities.Messages.ChatMessage;
using MySqlConnector.Authentication;
using Website.ViewModels;
using Website.ViewModels.Friends;
using Website.ViewModels.Messenger;
using Website.ViewModels.Messenger.Preview;
using Website.ViewModels.Messenger.Preview.Base;
using Website.ViewModels.Profile;
using Website.ViewModels.Users;

namespace Services
{
    public static class EntitiesConverterExtensions
    {
        #region User to UserViewModel
        /// <summary>
        /// Returns view model for User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static UserViewModel GetViewModel(this User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname??"",
            Patronymic = user.Patronymic??"",
            Birthday = user.Birthday,
            AvatarPath = user.AvatarPath,
            Country = user.Country.Name,
            AccessLevel = user.Role.Code.Equals("user") ? AccessLevel.user : user.Role.Code.Equals("mod") ? AccessLevel.mod : AccessLevel.admin
        };
        #endregion

        #region User to PreviewProfileViewModel
        /// <summary>
        /// Returns preview profile view model for User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static PreviewProfileViewModel GetPreviewViewModel(this User user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            AvatarPath = user.AvatarPath
        };
        #endregion

        #region UserPost to UserPostViewModel
        /// <summary>
        /// Returns view model for UserPost
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static UserPostViewModel GetViewModel(this UserPost p) => new()
        {
            Id = p.Id,
            CreatedDateTime = p.CreatedDateTime,
            Description = p.Description,
            Title = p.Title,
            Owner = p.Owner.GetViewModel()
        };
        #endregion

        #region SystemPost to SystemPostViewModel
        /// <summary>
        /// Returns view model for SystemPost
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static SystemPostViewModel GetViewModel(this SystemPost p) =>
            new()
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                CreationDateTime = p.CreatedDateTime
            };

        #endregion

        #region PostReport to PostReportViewModel
        /// <summary>
        /// Returns view model for PostReport
        /// </summary>
        /// <param name="pr"></param>
        /// <returns></returns>
        public static PostReportViewModel GetViewModel(this PostReport pr) => new()
        {
            Id = pr.Id,
            Sender = pr.Sender.GetViewModel(),
            Post = pr.Post.GetViewModel(),
            Commentary = pr.Commentary,
            ReportType = pr.ReportType.GetViewModel(),
            ReportState = pr.ReportState.GetViewModel()
        };
        #endregion

        #region ReportType to ReportTypeVM
        /// <summary>
        /// Returns view model for ReportType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ReportTypeVM GetViewModel(this ReportType type) => new()
        {
            Id = type.Id,
            Name = type.Name
        };
        #endregion

        #region ReportState to ReportStateVM
        /// <summary>
        /// Returns view model for ReportState
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static ReportStateVM GetViewModel(this ReportState state) => new()
        {
            Id = state.Id,
            Name = state.Name
        };
        #endregion

        #region Chat to ViewModel
        public static ChatViewModel GetViewModel(this Chat chat, int userId) => new()
        {
            Id = chat.Id,
            History = from message in chat.GetHistory() select message.GetViewModel(), 
            ConnectedUserId = userId,
            IsPrivateChat = chat.IsPrivateChat,
            Receiver = chat.GetReceiverChatParticipant(userId).User.GetViewModel()
        };
        #endregion

        #region IMessage to ViewModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static MessageViewModel GetViewModel(this IMessage message) =>
            message switch
            {
                ParticipantChatMessage userMessage => userMessage.GetViewModel(),
                GeneratedChatMessage gMessage => gMessage.GetViewModel(),
                _ => null
            };
        #endregion

        #region ParticipantChatMessage to UserMessageViewModel
        /// <summary>
        /// Returns view model for ParticipantChatMessage
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static UserMessageViewModel GetViewModel(this ParticipantChatMessage m) => new()
        {
            Id = m.Id,
            ChatParticipantSender = new()
            {
                User = m.ChatParticipantSender.User.GetViewModel(),
                CreatedDateTime = m.ChatParticipantSender.CreatedDateTime
            },
            CreatedDateTime = m.CreatedDateTime,
            Text = m.Text
        };
        #endregion

        #region GeneratedChatMessage to MessageViewModel
        /// <summary>
        /// Returns view model for GeneratedChatMessage
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static MessageViewModel GetViewModel(this GeneratedChatMessage m) => new()
        {
            Id = m.Id,
            CreatedDateTime = m.CreatedDateTime,
            Text = m.Text
        };
        #endregion

        #region Chat to PrivateChatPreviewViewModel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static PrivateChatPreviewViewModel GetPreviewViewModel(this Chat chat, int userId) => new()
        {
            Chat = chat.GetViewModel(userId),
            Receiver = chat.GetReceiverChatParticipant(userId).User.GetViewModel()
        };

        #endregion

        #region FriendshipApplication to FriendshipApplicationVM
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apl"></param>
        /// <returns></returns>
        public static FriendshipApplicationVM GetViewModel(this FriendshipApplication apl) => new()
        {
            UserOne = apl.UserOne.GetViewModel(),
            UserTwo = apl.UserTwo.GetViewModel(),
            ApplicationStateUserTwo = apl.ApplicationStateUserTwo.GetViewModel(),
            ApplicationStateUserOne = apl.ApplicationStateUserOne.GetViewModel()
        };
        #endregion

        #region FriendshipState to FriendshipStateVM
        /// <summary>
        /// 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private static FriendshipStateVM GetViewModel(this FriendshipState state) => state switch
        {
            FriendshipState.Allow => FriendshipStateVM.Allow,
            FriendshipState.Suspence => FriendshipStateVM.Suspence,
            FriendshipState.Deny => FriendshipStateVM.Deny,
            _ => throw new ArgumentOutOfRangeException()
        };
        #endregion

        #region FriendshipApplicationVM to FriendViewModel

        public static FriendViewModel GetFriendViewModel(this FriendshipApplicationVM application, int userId)
        {
            if (application == null) return null;
            var friend = application.UserOne.Id.Equals(userId)? application.UserTwo : application.UserOne;
            var f = new FriendViewModel
            {
                Id = application.Id,
                UserId = friend.Id,
                Name = friend.Name,
                Surname = friend.Surname ?? "",
                Patronymic = friend.Patronymic ?? "",
                AvatarPath = friend.AvatarPath,
                ApplicationState = application.GetApplicationState(userId)
            };
            return f;
        }
        #endregion

        #region MyRegion
        
        public static FriendViewModel GetFriendViewModel(this FriendshipApplicationVM application, int userId, ApplicationState state)
        {
            var friend = application.UserOne.Id.Equals(userId) ? application.UserTwo : application.UserOne;
            
            return state switch
            {
                ApplicationState.friend when !application.IsFriend(friend.Id) => null,
                ApplicationState.incoming when !application.IsIncoming(friend.Id) => null,
                ApplicationState.outgoing when !application.IsOutgoing(friend.Id) => null,
                _ => new FriendViewModel
                {
                    Id = application.Id,
                    UserId = friend.Id,
                    Name = friend.Name,
                    Surname = friend.Surname ?? "",
                    Patronymic = friend.Patronymic ?? "",
                    AvatarPath = friend.AvatarPath,
                    ApplicationState = state
                }
            };
        }

        #endregion

    }
}
