using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels.Friends;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class FriendsController : Controller
    {
        #region Get connected user id
        /// <summary>
        /// Получить подключенного пользователя с помощью claims
        /// </summary>
        private int GetConnectedUserID => int.Parse(User.Claims.First(claim => claim.Type.Equals("id")).Value);
        #endregion

        private IFriendsService FriendsService;
        public FriendsController(IFriendsService friendsService)
        {
            FriendsService = friendsService;
        }
        [Route("/friends")]
        public IActionResult Friends(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            return View("Friends", FriendsService.GetUserFriends(id));
        }

        [Route("/friends/remove")]
        public IActionResult TryRemoveFriendship(int target)
        {
            int id = GetConnectedUserID;
            if (FriendsService.TryRemoveUserFriendship(id, target)) return View("Friends", FriendsService.GetUserFriends(id));
            else return View("Friends", FriendsService.GetUserFriends(id)); // Error not removed
        }
        [Route("/friends/incoming")]
        public IActionResult IncomingFriendRequests(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            return View("FriendsIncoming", FriendsService.GetUserIncomingFriendships(id));
        }
        [Route("/friends/outgoing")]
        public IActionResult OutgoingFriendRequests(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            return View("FriendsOutgoing", FriendsService.GetUserOutgoingFriendships(id));
        }
    }
}
