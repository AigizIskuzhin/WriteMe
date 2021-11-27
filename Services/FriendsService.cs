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
            if (application is null) return null;
            var aplVM = application.GetViewModel();
            return aplVM.GetFriendViewModel(currentUserId);
        }
        public IEnumerable<FriendViewModel> GetUserFriends(int userId)
        {
            var applications = _Friendship.Items.Where(a => a.UserOne.Id.Equals(userId) || a.UserTwo.Id.Equals(userId));
            foreach (var a in applications)
            {
                var aplVM = a.GetViewModel();
                var friend = aplVM.GetFriendViewModel(userId, ApplicationState.friend);
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
                var friend =  aplVM.GetFriendViewModel(userId, ApplicationState.incoming);
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
                var friend =  aplVM.GetFriendViewModel(userId, ApplicationState.outgoing);
                yield return friend;
            }
        }

        // Отмена исходящей заявки
        public bool TryRemoveOutgoingFriendship(int id)
        {
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

            if (t.UserOne.Id.Equals(receiverId))
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
