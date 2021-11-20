using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Services.Interfaces;
using Website.Controllers.Rules;
using Website.Infrastructure.Extensions;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class FriendsController : Controller
    {
        #region Get connected user id
        /// <summary>
        /// Получить подключенного пользователя с помощью claims
        /// </summary>
        private int GetConnectedUserID => int.Parse(User.GetConnectedUserId()); 
        #endregion
        private readonly IFriendsService FriendsService;
        public FriendsController(IFriendsService friendsService)
        {
            FriendsService = friendsService;
        }
        [Route("/friends")]
        public IActionResult Friends(int id)
        {
            if (id == 0) id = int.Parse(HttpContext.GetConnectedUserId());
            return View("Friends", FriendsService.GetUserFriends(id));
        }

        [Route("/friends/remove")]
        public IActionResult TryRemoveFriendship(int target)
        {
            int id = GetConnectedUserID;
            FriendsService.TryRemoveUserFriendship(id, target);
            return View("Friends", FriendsService.GetUserFriends(id)); // Error not removed
        }
        [Route("/friends/incoming")]
        public IActionResult IncomingFriendRequests(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            return View("FriendsIncoming", FriendsService.GetIncomingApplications(id));
        }
        [Route("/friends/outgoing")]
        public IActionResult OutgoingFriendRequests(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            return View("FriendsOutgoing", FriendsService.GetOutgoingApplications(id));
        }

        [Route("/friends/remove")]
        public IActionResult TryRemoveUserFriendship(int userId, int targetUserId)
        {
            FriendsService.TryRemoveUserFriendship(userId, targetUserId);
                return View("Friends", FriendsService.GetUserFriends(userId));
        }

        [Route("/friends/outgoing/remove")]
        public IActionResult TryRemoveOutgoingFriendship(int id)
        {
            FriendsService.TryRemoveOutgoingFriendship(id);
            return View("FriendsOutgoing", FriendsService.GetOutgoingApplications(GetConnectedUserID));
        }
        [Route("/friends/incoming/allow")]
        public IActionResult AllowIncomingFriendship(int userId, int targetUserId)
        {
            FriendsService.TryAllowIncomingFriendship(userId, targetUserId);
            return View("Friends", FriendsService.GetUserFriends(userId));
        }
        [Route("/friends/incoming/deny")]
        public IActionResult DenyIncomingFriendship(int userId, int targetUserId)
        {
            FriendsService.TryDenyIncomingFriendship(userId, targetUserId);
            return View("Friends", FriendsService.GetUserFriends(userId));
        }

    }
}
