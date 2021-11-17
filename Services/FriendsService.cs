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

        public IEnumerable<FriendViewModel> GetUserFriends(int userId)
        {
            return from a in _Friendship.Items.Where(a =>
                    (a.UserOne.Id == userId || a.UserTwo.Id == userId) &&
                    a.ApplicationStateUserOne == FriendshipStates.Allow &&
                    a.ApplicationStateUserTwo == FriendshipStates.Allow)
                select ConstructFriend(userId, a);
        }

        // Запрос друзей по ФИО
        public IEnumerable<FriendViewModel> GetUserFriends(int userId, string filterString) =>
            from a in _Friendship.Items.Where(a =>
                (a.UserOne.Id == userId || a.UserTwo.Id == userId) &&
                a.ApplicationStateUserOne == FriendshipStates.Allow &&
                a.ApplicationStateUserTwo == FriendshipStates.Allow) 
            select ConstructFriend(userId, a);

        public FriendViewModel GetFriendViewModel(int userId, int targetUserId)
        {
            var sender = UsersRepository.Get(userId);
            var receiver = UsersRepository.Get(targetUserId);
            var friendship = _Friendship.Items.FirstOrDefault(f =>
                f.UserOne.Id.Equals(sender.Id) && f.UserTwo.Id.Equals(receiver.Id) ||
                f.UserOne.Id.Equals(receiver.Id) && f.UserTwo.Id.Equals(sender.Id));
            return friendship==null?null:ConstructFriend(userId, friendship);
        }

        // Запрос входящих заявок
        public IEnumerable<FriendViewModel> GetIncomingApplications(int userId)
        {
            foreach (var application in _Friendship.Items.Where(application =>
                application.UserTwo.Id == userId &&
                application.ApplicationStateUserTwo == FriendshipStates.Suspence &&
                application.ApplicationStateUserOne == FriendshipStates.Allow))
            {
                yield return ConstructFriend(userId, application, state:state.incoming);
            }
        }

        // Запрос исходящих заявок
        public IEnumerable<FriendViewModel> GetOutgoingApplications(int userId)
        {
            return from user in _Friendship.Items select ConstructFriend(userId, user);
            //foreach (var application in _Friendship.Items.Where(application =>
            //    application.UserOne.Id == userId &&
            //    application.ApplicationStateUserOne == FriendshipStates.Allow &&
            //    application.ApplicationStateUserTwo == FriendshipStates.Suspence))
            //{
            //    yield return ConstructFriend(userId, application, state:state.outgoing);
            //}
        }

        // Отмена исходящей заявки
        public bool TryRemoveOutgoingFriendship(int userId, int targetUserId)
        {
            List<FriendshipApplication> targetFriendship = _Friendship.Items.Where(application =>
                application.UserOne.Id == userId &&
                application.ApplicationStateUserOne == FriendshipStates.Allow &&
                application.ApplicationStateUserTwo == FriendshipStates.Suspence).ToList();
            if (targetFriendship.Count > 0)
            {
                targetFriendship[0].ApplicationStateUserOne = FriendshipStates.Deny;
                _Friendship.Remove(targetFriendship[0].Id);
                return true;
            }
            else return false;
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
            if (t!=null)
            {
                if(UserOneIsNotReceiver(t, receiverId))
                    t.ApplicationStateUserTwo = responseState;
                else t.ApplicationStateUserOne = responseState;
                _Friendship.Update(t);
                return true;
            }
            return false;
        }

        private bool UserOneIsNotReceiver(FriendshipApplication friendshipApplication, int userId) =>
            friendshipApplication.UserOne.Id.Equals(userId);

        public bool TryAllowIncomingFriendship(int id, int receiverId) =>
            TryResponseIncomingFriendship(id, receiverId, FriendshipStates.Allow);

        public bool TryDenyIncomingFriendship(int id, int receiverId) =>
            TryResponseIncomingFriendship(id, receiverId, FriendshipStates.Deny);

        public bool TrySendFriendshipRequest(int userId, int targetUserId)
        {
            var sender = UsersRepository.Get(userId);
            var receiver = UsersRepository.Get(targetUserId);

            if (!_Friendship.Items.Any(f => f.UserOne.Id.Equals(sender.Id) && f.UserTwo.Id.Equals(receiver.Id) ||
                                            f.UserOne.Id.Equals(receiver.Id) && f.UserTwo.Id.Equals(sender.Id)))
            {
                var t = _Friendship.Add(new FriendshipApplication
                {
                    UserOne = sender,
                    ApplicationStateUserOne = FriendshipStates.Allow,
                    UserTwo = receiver,
                    ApplicationStateUserTwo = FriendshipStates.Suspence
                });
                return t != null;
            }
            return false;
        }

        
        // Сборка FriendViewModel
        private FriendViewModel ConstructFriend(int userId, FriendshipApplication application, string filterString=null, state state = state.nil)
        {
            var friend = UserOneIsNotReceiver(application, userId)
                ? application.UserOne
                : application.UserTwo;

            return friend.GetFriendViewModel();
            bool oneEqualsId = application.UserOne.Id == userId;

            string targetName = oneEqualsId ? application.UserTwo.Name + " " + application.UserTwo.Surname + " " + application.UserTwo.Patronymic
                : application.UserOne.Name + " " + application.UserOne.Surname + " " + application.UserOne.Patronymic;
            if (filterString!=null&&!targetName.Contains(filterString)) return null;
            targetName = targetName.Replace("  ", " ");
            return new FriendViewModel
            {
                Id = oneEqualsId ? application.UserTwo.Id : application.UserOne.Id,
                Name = targetName,
                PhotoPath = "",
                FriendshipApplication = application,
                State= state
            };
        }

        public enum state
        {
            incoming=0,
            outgoing=1,
            nil=2
        }

        private static class UserExtensions
        {
            public static FriendViewModel GetFriendViewModel(this User friend) => new FriendViewModel
            {
                Name = friend.Name,
                Surname = friend.Surname,
                Patronymic = friend.Patronymic,
                PhotoPath = friend.AvatarPath,

            };
        }
    }
}
