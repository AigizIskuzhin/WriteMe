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

        #region Services

        private readonly IFriendsService FriendsService; 
        #endregion

        #region ctor
        public FriendsController(IFriendsService friendsService)
        {
            FriendsService = friendsService;
        } 
        #endregion

        #region Friends
        [Route("/friends")]
        public IActionResult Friends(int id)
        {
            if (id == 0) id = int.Parse(HttpContext.GetConnectedUserId());
            ViewData["isOwner"] = id == GetConnectedUserID;
            return View("Friends", FriendsService.GetUserFriends(id));
        }

        #endregion

        #region Get filtred friends

        [Route("/friends/search")]
        public IActionResult SearchFriends(string filter, int userId)
        {
            userId = userId == 0 ? GetConnectedUserID : userId;
            ViewData["isOwner"] = userId == GetConnectedUserID;
            return View("FriendsView", string.IsNullOrWhiteSpace(filter) ? FriendsService.GetUserFriends(userId):
                FriendsService.GetUserFriends(userId, filter));
        }

        #endregion

        #region Remove friend
        [Route("/friends/remove")]
        public IActionResult TryRemoveFriendship(int target)
        {
            int id = GetConnectedUserID;
            FriendsService.TryRemoveUserFriendship(id, target);
            return RedirectToAction("Friends");
        }
        #endregion

        #region Outgoing friend requests
        [Route("/friends/outgoing")]
        public IActionResult OutgoingFriendRequests(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            ViewData["isOwner"] = id == GetConnectedUserID;
            return View("FriendsOutgoing", FriendsService.GetOutgoingApplications(id));
        }
        #endregion

        #region Remove outgoing friend request
        [Route("/friends/outgoing/remove")]
        public IActionResult TryRemoveOutgoingFriendship(int id)
        {
            FriendsService.TryRemoveOutgoingFriendship(id);
            return RedirectToAction("OutgoingFriendRequests");
        }
        #endregion

        #region Incoming friend requests
        [Route("/friends/incoming")]
        public IActionResult IncomingFriendRequests(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            ViewData["isOwner"] = id == GetConnectedUserID;
            return View("FriendsIncoming", FriendsService.GetIncomingApplications(id));
        }
        #endregion

        #region Allow incoming friend request   
        [Route("/friends/incoming/allow")]
        public IActionResult AllowIncomingFriendship(int id)
        {
            FriendsService.TryAllowIncomingFriendship(id, GetConnectedUserID);
            return RedirectToAction("IncomingFriendRequests");
        }
        #endregion

        #region Deny incoming friend request
        [Route("/friends/incoming/deny")]
        public IActionResult DenyIncomingFriendship(int target)
        {
            FriendsService.TryDenyIncomingFriendship(target, GetConnectedUserID);
            return RedirectToAction("IncomingFriendRequests");
        } 
        #endregion

    }
}
