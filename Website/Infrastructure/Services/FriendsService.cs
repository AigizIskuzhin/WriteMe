using Database.DAL.Entities;
using Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels.Friends;

namespace Website.Infrastructure.Services
{
    public class FriendsService : IFriendsService
    {
        private IRepository<FriendshipApplication> _Friendship { get; }
        public FriendsService(IRepository<FriendshipApplication> friendship)
        {
            _Friendship = friendship;
        }

        // Запрос друзей
        public IEnumerable<FriendViewModel> GetUserFriends(int userId)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));
            foreach (var application in _Friendship.Items.Where(application =>
                (application.UserOne.Id == userId || application.UserTwo.Id == userId) &&
                application.ApplicationStateUserOne == FriendshipStates.Allow &&
                application.ApplicationStateUserTwo == FriendshipStates.Allow))
            {
                bool oneEqualsId = application.UserOne.Id == userId;

                string targetName = oneEqualsId ? application.UserTwo.Name + " " + application.UserTwo.Surname + " " + application.UserTwo.Patronymic
                    : application.UserOne.Name + " " + application.UserOne.Surname + " " + application.UserOne.Patronymic;
                targetName.Replace("  ", " ");
                yield return new()
                {
                    Id = oneEqualsId ? application.UserTwo.Id : application.UserOne.Id,
                    Name = targetName,
                    PhotoPath = ""
                };
            }


        }

        // Запрос друзей по ФИО
        public IEnumerable<FriendViewModel> GetUserFilteredFriends(int userId, string filterString)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));
            foreach (var application in _Friendship.Items.Where(application =>
                application.UserOne.Id == userId || application.UserTwo.Id == userId))
            {
                bool oneEqualsId = application.UserOne.Id == userId;

                string targetName = oneEqualsId ? application.UserTwo.Name + " " + application.UserTwo.Surname + " " + application.UserTwo.Patronymic
                    : application.UserOne.Name + " " + application.UserOne.Surname + " " + application.UserOne.Patronymic;
                if (!targetName.Contains(filterString)) continue;
                targetName.Replace("  ", " ");
                yield return new()
                {
                    Id = oneEqualsId ? application.UserTwo.Id : application.UserOne.Id,
                    Name = targetName,
                    PhotoPath = ""
                };
            }
        }

        // Запрос входящих заявок
        public IEnumerable<FriendViewModel> GetUserIncomingFriendships(int userId)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));
            foreach (var application in _Friendship.Items.Where(application =>
                application.UserTwo.Id == userId &&
                application.ApplicationStateUserTwo == FriendshipStates.Suspence &&
                application.ApplicationStateUserOne == FriendshipStates.Allow))
            {
                bool oneEqualsId = application.UserOne.Id == userId;

                string targetName = oneEqualsId ? application.UserTwo.Name + " " + application.UserTwo.Surname + " " + application.UserTwo.Patronymic
                    : application.UserOne.Name + " " + application.UserOne.Surname + " " + application.UserOne.Patronymic;
                targetName.Replace("  ", " ");
                yield return new()
                {
                    Id = oneEqualsId ? application.UserTwo.Id : application.UserOne.Id,
                    Name = targetName,
                    PhotoPath = ""
                };
            }
        }

        // Запрос исходящих заявок
        public IEnumerable<FriendViewModel> GetUserOutgoingFriendships(int userId)
        {
            if (userId == 0)
                throw new ArgumentNullException(nameof(userId));
            foreach (var application in _Friendship.Items.Where(application =>
                application.UserOne.Id == userId &&
                application.ApplicationStateUserOne == FriendshipStates.Allow &&
                application.ApplicationStateUserTwo == FriendshipStates.Suspence))
            {
                bool oneEqualsId = application.UserOne.Id == userId;

                string targetName = oneEqualsId ? application.UserTwo.Name + " " + application.UserTwo.Surname + " " + application.UserTwo.Patronymic
                    : application.UserOne.Name + " " + application.UserOne.Surname + " " + application.UserOne.Patronymic;
                targetName.Replace("  ", " ");
                yield return new()
                {
                    Id = oneEqualsId ? application.UserTwo.Id : application.UserOne.Id,
                    Name = targetName,
                    PhotoPath = ""
                };
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
                bool oneEqualsId = targetFriendship[0].UserOne.Id == userId;
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
                bool oneEqualsId = targetFriendship[0].UserOne.Id == userId;
                targetFriendship[0].ApplicationStateUserOne = FriendshipStates.Deny;
                _Friendship.Update(targetFriendship[0]);
                return true;
            }
            return false;
        }

        // Ответ на входящую заявку
        public bool TryResponseIncomingFriendship(int userId, int targetUserId, FriendshipStates responseState)
        {
            List<FriendshipApplication> targetFriendship = _Friendship.Items.Where(application =>
                application.UserTwo.Id == userId &&
                application.UserOne.Id == targetUserId &&
                application.ApplicationStateUserOne == FriendshipStates.Suspence &&
                application.ApplicationStateUserTwo == FriendshipStates.Allow).ToList();
            if (targetFriendship.Count > 0)
            {
                targetFriendship[0].ApplicationStateUserTwo = responseState;
                _Friendship.Update(targetFriendship[0]);
                return true;
            }
            else return false;
        }
    }
}
