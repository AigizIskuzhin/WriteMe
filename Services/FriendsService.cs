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
        public static UserViewModel GetViewModel(User u) => new()
        {
            Id=u.Id,
            Name = u.Name,
            Surname = u.Surname,
            Patronymic = u.Patronymic,
            Birthday = u.Birthday,
            AvatarPath = u.AvatarPath
        };
        public FriendshipApplicationVM GetViewModel(FriendshipApplication apl)
        {
            FriendshipApplicationVM t = new()
            {
                UserOne = GetViewModel(apl.UserOne),
                UserTwo = GetViewModel(apl.UserTwo),
                ApplicationStateUserTwo = apl.ApplicationStateUserTwo switch
                {
                    FriendshipStates.Allow => FriendshipStatesVM.Allow,
                    FriendshipStates.Suspence => FriendshipStatesVM.Suspence,
                    FriendshipStates.Deny => FriendshipStatesVM.Deny,
                    _ => throw new ArgumentOutOfRangeException()
                },
                ApplicationStateUserOne = apl.ApplicationStateUserOne switch
                {
                    FriendshipStates.Allow => FriendshipStatesVM.Allow,
                    FriendshipStates.Suspence => FriendshipStatesVM.Suspence,
                    FriendshipStates.Deny => FriendshipStatesVM.Deny,
                    _ => throw new ArgumentOutOfRangeException()
                }
            };
            return t;
        }
        public FriendViewModel GetFriendViewModel(int currentUserId, int userId)
        {
            var application = _Friendship.Items.FirstOrDefault(a =>
                a.UserOne.Id.Equals(currentUserId) && a.UserTwo.Id.Equals(userId) ||
                a.UserOne.Id.Equals(userId) && a.UserTwo.Id.Equals(currentUserId));
            var aplVM = GetViewModel(application);

            return GetFriendViewModel(aplVM, currentUserId);
        }
        public FriendViewModel GetFriendViewModel(FriendshipApplicationVM application, int userId)
        {
            if (application == null) return null;
            var friend = GetFriendFromApplication(application, userId);
            var f = new FriendViewModel
            {
                Id = application.Id,
                UserId = friend.Id,
                Name = friend.Name,
                Surname = friend.Surname ?? "",
                Patronymic = friend.Patronymic ?? "",
                AvatarPath = friend.AvatarPath
            };
            if (application.IsFriend(friend.Id))
                f.ApplicationState = ApplicationState.friend;
            if (application.IsIncoming(friend.Id))
                f.ApplicationState = ApplicationState.incoming;
            if (application.IsOutgoing(friend.Id))
                f.ApplicationState = ApplicationState.outgoing;
            return f;
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
                var aplVM = GetViewModel(a);
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
                var aplVM = GetViewModel(a);
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
                var aplVM = GetViewModel(a);
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

            application.ApplicationStateUserOne = FriendshipStates.Suspence;
            _Friendship.Update(application);
            return true;
        }

        // Ответ на входящую заявку
        private bool TryResponseIncomingFriendship(int id, int receiverId, FriendshipStates responseState)
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
            TryResponseIncomingFriendship(id, receiverId, FriendshipStates.Allow);

        public bool TryDenyIncomingFriendship(int id, int receiverId) =>
            TryResponseIncomingFriendship(id, receiverId, FriendshipStates.Deny);

        public bool TrySendFriendshipRequest(int userId, int targetUserId)
        {
            var sender = UsersRepository.Get(userId);
            var receiver = UsersRepository.Get(targetUserId);

            if (_Friendship.Items.FirstOrDefault(f =>
                f.UserOne.Id.Equals(sender.Id) && f.UserTwo.Id.Equals(receiver.Id) ||
                f.UserOne.Id.Equals(receiver.Id) && f.UserTwo.Id.Equals(sender.Id)) !=null)
                return false;
            var t = _Friendship.Add(new FriendshipApplication
            {
                UserOne = sender,
                ApplicationStateUserOne = FriendshipStates.Allow,
                UserTwo = receiver,
                ApplicationStateUserTwo = FriendshipStates.Suspence
            });
            return t != null;

        }

    }
}
