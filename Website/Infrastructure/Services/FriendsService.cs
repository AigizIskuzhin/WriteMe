using Database.DAL.Entities;
using Database.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels.Friends;

namespace Website.Infrastructure.Services
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


        // Запрос друзей
        public IEnumerable<FriendViewModel> GetUserFriends(int userId)
        {
            foreach (var application in _Friendship.Items.Where(application =>
                (application.UserOne.Id == userId || application.UserTwo.Id == userId) &&
                application.ApplicationStateUserOne == FriendshipStates.Allow &&
                application.ApplicationStateUserTwo == FriendshipStates.Allow))
            {
                yield return ConstructFriend(userId, application);
            }


        }

        // Запрос друзей по ФИО
        public IEnumerable<FriendViewModel> GetUserFilteredFriends(int userId, string filterString)
        {
            foreach (var application in _Friendship.Items.Where(application =>
                application.UserOne.Id == userId || application.UserTwo.Id == userId))
            {
                var friend = ConstructFriend(userId, application,filterString);
                if (friend != null)
                    yield return friend;
                else 
                    continue;
            }
        }

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
        public IEnumerable<FriendViewModel> GetUserIncomingFriendships(int userId)
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
        public IEnumerable<FriendViewModel> GetUserOutgoingFriendships(int userId)
        {
            foreach (var application in _Friendship.Items.Where(application =>
                application.UserOne.Id == userId &&
                application.ApplicationStateUserOne == FriendshipStates.Allow &&
                application.ApplicationStateUserTwo == FriendshipStates.Suspence))
            {
                yield return ConstructFriend(userId, application, state:state.outgoing);
            }
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
        private bool TryResponseIncomingFriendship(int userId, int targetUserId, FriendshipStates responseState)
        {
            var t = GetFriendViewModel(userId, targetUserId);
            if (t.FriendshipApplication!=null)
            {
                if (t.FriendshipApplication.UserOne.Id.Equals(userId))
                    t.FriendshipApplication.ApplicationStateUserOne = responseState;
                else t.FriendshipApplication.ApplicationStateUserTwo = responseState;
                _Friendship.Update(t.FriendshipApplication);
                return true;
            }
            else return false;
        }
        
        public bool TryAllowIncomingFriendship(int userId, int targetUserId)
        {
            return TryResponseIncomingFriendship(userId, targetUserId, FriendshipStates.Allow);
        }
        public bool TryDenyIncomingFriendship(int userId, int targetUserId)
        {
            return TryResponseIncomingFriendship(userId, targetUserId, FriendshipStates.Deny);
        }

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
            bool oneEqualsId = application.UserOne.Id == userId;

            string targetName = oneEqualsId ? application.UserTwo.Name + " " + application.UserTwo.Surname + " " + application.UserTwo.Patronymic
                : application.UserOne.Name + " " + application.UserOne.Surname + " " + application.UserOne.Patronymic;
            if (filterString!=null&&!targetName.Contains(filterString)) return null;
            targetName.Replace("  ", " ");
            return new()
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
    }
}
