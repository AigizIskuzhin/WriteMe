using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Linq;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;

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
        public // IEnumerable<FriendViewModel>
            IActionResult Friends(int id)
        {
            if (id == 0) id = GetConnectedUserID;
            return View("Friends", FriendsService.GetUserFriends(id));
            //FriendsService.GetUserFriends(id).ToList()[0].Name;
        }

        [Route("/friends.RemoveFriendship")]
        public string TryRemoveFriendship(int target)
        {
            int id = GetConnectedUserID;
            if (FriendsService.TryRemoveUserFriendship(id, target)) return "Removed";
            else return "NotRemoved";
        }
        public IActionResult IncomingFriendRequests()
        {
            return View("IncomingFriendshipRequests");
        }

        public IActionResult OutgoingFriendRequests()
        {
            return View("OutgoingFriendshipRequests");
        }
    }
}
