using Database.DAL.Entities;
using Database.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Website.ViewModels.Friends;

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

            return GetFriendViewModel(application, userId);
        }
        public FriendViewModel GetFriendViewModel(FriendshipApplication application, int userId)
        {
            var friend = GetFriendFromApplication(application, userId);
            var f = new FriendViewModel
            {
                Id = application.Id,
                Name = friend.Name,
                Surname = friend.Surname,
                Patronymic = friend.Patronymic,
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
        public FriendViewModel GetFriendViewModel(FriendshipApplication application, int userId, ApplicationState state)
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
                    Name = friend.Name,
                    Surname = friend.Surname,
                    Patronymic = friend.Patronymic,
                    AvatarPath = friend.AvatarPath,
                    ApplicationState = state
                }
            };
        }
        private User GetFriendFromApplication(FriendshipApplication application, int userId) =>
            UserOneIsNotSender(application, userId) ? application.UserTwo : application.UserOne;
        private bool UserOneIsNotSender(FriendshipApplication application, int userId) =>
            application.UserOne.Id.Equals(userId);

        public IEnumerable<FriendViewModel> GetUserFriends(int userId)
        {
            var applications = _Friendship.Items.Where(a => a.UserOne.Id.Equals(userId) || a.UserTwo.Id.Equals(userId));
            foreach (var a in applications)
            {
                var friend = GetFriendViewModel(a, userId,
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
                var friend = GetFriendViewModel(a, userId,
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
                var friend = GetFriendViewModel(a, userId,
                    ApplicationState.outgoing);
                if (friend is not null)
                    yield return friend;
            }
        }

        // Отмена исходящей заявки
        public bool TryRemoveOutgoingFriendship(int id)
        {
            var application = _Friendship.Get(id);
            if (application == null) return false;
            
            application.ApplicationStateUserOne = FriendshipStates.Suspence;

            //_Friendship.Remove(id);
            return true;
        }

        // Удаление друга
        public bool TryRemoveUserFriendship(int userId, int targetUserId)
        {
            List<FriendshipApplication> targetFriendship = _Friendship.Items.Where(application =>
                application.UserOne.Id == userId && application.UserTwo.Id == targetUserId &&
                application.ApplicationStateUserOne == FriendshipStates.Allow ||
                application.UserOne.Id == targetUserId && application.UserTwo.Id == userId &&
                application.ApplicationStateUserTwo == FriendshipStates.Allow).ToList();
            if (targetFriendship.Count > 0)
            {
                targetFriendship[0].ApplicationStateUserOne = FriendshipStates.Deny;
                _Friendship.Update(targetFriendship[0]);
                return true;
            }
            return false;
        }

        // Ответ на входящую заявку
        private bool TryResponseIncomingFriendship(int id, int receiverId, FriendshipStates responseState)
        {
            var t = _Friendship.Get(id);
            if (t == null) return false;

            if(UserOneIsNotSender(t, receiverId))
                t.ApplicationStateUserTwo = responseState;
            else t.ApplicationStateUserOne = responseState;

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

            if (_Friendship.Items.Any(f =>
                f.UserOne.Id.Equals(sender.Id) && f.UserTwo.Id.Equals(receiver.Id) ||
                f.UserOne.Id.Equals(receiver.Id) && f.UserTwo.Id.Equals(sender.Id)))
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
