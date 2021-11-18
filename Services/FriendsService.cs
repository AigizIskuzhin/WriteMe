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

        public IEnumerable<FriendViewModel> GetUserFriends(int userId) =>
            from a in _Friendship.Items.Where(a =>
                (a.UserOne.Id == userId || a.UserTwo.Id == userId) &&
                a.ApplicationStateUserOne == FriendshipStates.Allow &&
                a.ApplicationStateUserTwo == FriendshipStates.Allow)
            select GetFriendViewModel(a, userId, ApplicationState.friend);

        // Запрос друзей по ФИО
        public IEnumerable<FriendViewModel> GetUserFriends(int userId, string filter) =>
            GetUserFriends(userId).Where(a => a.IsFitsCondition(filter));

        // Запрос входящих заявок
        public IEnumerable<FriendViewModel> GetIncomingApplications(int userId) => from application in _Friendship.Items
            select GetFriendViewModel(application, userId, ApplicationState.incoming);

        // Запрос исходящих заявок
        public IEnumerable<FriendViewModel> GetOutgoingApplications(int userId) => from application in _Friendship.Items
            select GetFriendViewModel(application, userId, ApplicationState.outgoing);

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

        
        
        public FriendViewModel GetFriendViewModel(FriendshipApplication application, int userId, ApplicationState state = ApplicationState.friend)
        {
            var friend = GetFriendFromApplication(application, userId);
            if(state == ApplicationState.friend)
                if (!application.IsFitCondition(friend.Id))
                    return null;

            return new()
            {
                Name = friend.Name,
                Surname = friend.Surname,
                Patronymic = friend.Patronymic,
                PhotoPath = friend.AvatarPath,
                ApplicationState = state
            };
        }
        private User GetFriendFromApplication(FriendshipApplication application, int userId) =>
            UserOneIsNotReceiver(application, userId) ? application.UserOne : application.UserTwo;
        private bool UserOneIsNotReceiver(FriendshipApplication application, int userId) =>
            application.UserOne.Id.Equals(userId);
        private bool ApplicationStateIs(FriendshipApplication a, int userId, ApplicationState state = ApplicationState.friend)
        {
            return false;
            //switch (state)
            //{
            //    case ApplicationState.friend:
            //        return a.ApplicationStateUserOne == a.ApplicationStateUserTwo &&
            //               a.ApplicationStateUserTwo == FriendshipStates.Allow;
            //    case ApplicationState.outgoing:
            //        return UserOneIsNotReceiver(a, userId)
            //}
        }

        private bool ApplicationStateIs(FriendshipApplication a, int userId, FriendshipStates state)
        {
            return false;
            //    switch (state)
            //    {
            //        case FriendshipStates.Allow:
            //            return UserOneIsNotReceiver(a, userId)
            //                ? a.ApplicationStateUserOne == FriendshipStates.Suspence &&
            //                  a.ApplicationStateUserTwo == FriendshipStates.Allow
            //                : a.ApplicationStateUserOne == FriendshipStates.Allow &&
            //                  a.ApplicationStateUserTwo == FriendshipStates.Suspence;
            //        case FriendshipStates.Suspence:
            //            return UserOneIsNotReceiver(a, userId)
            //                ? a.ApplicationStateUserOne == FriendshipStates.Suspence &&
            //                  a.ApplicationStateUserTwo == FriendshipStates.Allow;
            //    }
            //    return UserOneIsNotReceiver(a, userId)
            //        ? a.ApplicationStateUserOne == FriendshipStates.Suspence &&
            //          a.ApplicationStateUserTwo == FriendshipStates.Allow;
        }


        //private bool IsOutgoingApplication(FriendshipApplication a, int userId) =>
        //    UserOneIsNotReceiver(a, userId)
        //        ? a.ApplicationStateUserOne == FriendshipStates.Suspence &&
        //          a.ApplicationStateUserTwo == FriendshipStates.Allow
    }
}
