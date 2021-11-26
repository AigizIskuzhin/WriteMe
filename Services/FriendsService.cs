using System;
using Database.DAL.Entities;
using Database.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Website.ViewModels.Friends;
using Website.ViewModels.Users;

namespace Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IRepository<FriendshipApplication> _Friendship;
        private readonly IRepository<User> UsersRepository;
        public FriendsService(IRepository<FriendshipApplication> friendship, IRepository<User> usersRepository)
        {
            _Friendship = friendship;
            UsersRepository = usersRepository;
        }


        public FriendViewModel GetFriendViewModel(int currentUserId, int userId)
        {
            var application = _Friendship.Items.FirstOrDefault(a =>
                a.UserOne.Id.Equals(currentUserId) && a.UserTwo.Id.Equals(userId) ||
                a.UserOne.Id.Equals(userId) && a.UserTwo.Id.Equals(currentUserId));
            if (application is null) return null;
            var aplVM = application.GetViewModel();
            return aplVM.GetFriendViewModel(currentUserId);
        }
        public FriendViewModel GetFriendViewModel(FriendshipApplicationVM application, int userId, ApplicationState state)
        {
            var friend = GetFriendFromApplication(application, userId);
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
        private UserViewModel GetFriendFromApplication(FriendshipApplicationVM application, int userId) =>
            UserOneIsNotSender(application, userId) ? application.UserTwo : application.UserOne;
        private bool UserOneIsNotSender(FriendshipApplicationVM application, int userId) =>
            application.UserOne.Id.Equals(userId);
        private bool UserOneIsNotSender(FriendshipApplication application, int userId) =>
            application.UserOne.Id.Equals(userId);

        public IEnumerable<FriendViewModel> GetUserFriends(int userId)
        {
            var applications = _Friendship.Items.Where(a => a.UserOne.Id.Equals(userId) || a.UserTwo.Id.Equals(userId));
            foreach (var a in applications)
            {
                var aplVM = a.GetViewModel();
                var friend = GetFriendViewModel(aplVM, userId,
                    ApplicationState.friend);
                if (friend is not null)
                    yield return friend;
            }
        }

        // Запрос друзей по ФИО
        public IEnumerable<FriendViewModel> GetUserFriends(int userId, string filter) =>
            GetUserFriends(userId).Where(a => a.IsFitsCondition(filter));

        // Запрос входящих заявок
        public IEnumerable<FriendViewModel> GetIncomingApplications(int userId)
        {
            var applications = _Friendship.Items.Where(a => a.UserOne.Id.Equals(userId) || a.UserTwo.Id.Equals(userId));
            foreach (var a in applications)
            {
                var aplVM = a.GetViewModel();
                var friend = GetFriendViewModel(aplVM, userId,
                    ApplicationState.incoming);
                if (friend is not null)
                    yield return friend;
            }
        }

        // Запрос исходящих заявок
        public IEnumerable<FriendViewModel> GetOutgoingApplications(int userId)
        {
            var applications = _Friendship.Items.Where(a => a.UserOne.Id.Equals(userId) || a.UserTwo.Id.Equals(userId));
            foreach (var a in applications)
            {
                var aplVM = a.GetViewModel();
                var friend = GetFriendViewModel(aplVM, userId,
                    ApplicationState.outgoing);
                if (friend is not null)
                    yield return friend;
            }
        }

        // Отмена исходящей заявки
        public bool TryRemoveOutgoingFriendship(int id)
        {
            //var application = _Friendship.Get(id);
            //if (application == null) return false;

            //if(application.ApplicationStateUserOne == FriendshipStates.Allow)
            //    application.ApplicationStateUserOne = FriendshipStates.Suspence;
            //else application.ApplicationStateUserTwo = FriendshipStates.Suspence;
            //_Friendship.Update(application);
            _Friendship.Remove(id);
            return true;
        }

        // Удаление друга
        public bool TryRemoveUserFriendship(int id, int targetUserId)
        {
            var application = _Friendship.Get(id);

            if (application == null) return false;

            application.ApplicationStateUserOne = FriendshipState.Suspence;
            _Friendship.Update(application);
            return true;
        }

        // Ответ на входящую заявку
        private bool TryResponseIncomingFriendship(int id, int receiverId, FriendshipState responseState)
        {
            var t = _Friendship.Get(id);
            if (t == null) return false;

            if (UserOneIsNotSender(t, receiverId))
                t.ApplicationStateUserOne = responseState;
            else
                t.ApplicationStateUserTwo = responseState;

            _Friendship.Update(t);

            return true;
        }

        public bool TryAllowIncomingFriendship(int id, int receiverId) =>
            TryResponseIncomingFriendship(id, receiverId, FriendshipState.Allow);

        public bool TryDenyIncomingFriendship(int id, int receiverId) =>
            TryResponseIncomingFriendship(id, receiverId, FriendshipState.Deny);

        public bool TrySendFriendshipRequest(int userId, int targetUserId)
        {
            var sender = UsersRepository.Get(userId);
            var receiver = UsersRepository.Get(targetUserId);

            if (_Friendship.Items.FirstOrDefault(f =>
                f.UserOne.Id.Equals(sender.Id) && f.UserTwo.Id.Equals(receiver.Id) ||
                f.UserOne.Id.Equals(receiver.Id) && f.UserTwo.Id.Equals(sender.Id)) != null)
                return false;
            var t = _Friendship.Add(new FriendshipApplication
            {
                UserOne = sender,
                ApplicationStateUserOne = FriendshipState.Allow,
                UserTwo = receiver,
                ApplicationStateUserTwo = FriendshipState.Suspence
            });
            return t != null;

        }

    }
}
