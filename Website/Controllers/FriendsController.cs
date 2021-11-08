using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Website.Controllers.Rules;
using Website.Infrastructure.Extensions;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class FriendsController : Controller
    {
        private readonly IFriendsService FriendsService;
        public FriendsController(IFriendsService friendsService)
        {
            FriendsService = friendsService;
        }
        [Route("/friends")]
        public ActionResult Friends(int id)
        {
            if (id == 0) id = int.Parse(HttpContext.GetConnectedUserId());
            return View("Friends", FriendsService.GetUserFriends(id));
        }

        [Route("/friends.RemoveFriendship")]
        public bool TryRemoveFriendship(int target)
        {
            int id = int.Parse(HttpContext.GetConnectedUserId());
            return FriendsService.TryRemoveUserFriendship(id, target);
        }
        [Route("/requests.incoming")]
        public IActionResult IncomingFriendRequests()
        {
            return View("IncomingFriendshipRequests");
        }
        
        [Route("/requests.outgoing")]
        public IActionResult OutgoingFriendRequests()
        {
            return View("OutgoingFriendshipRequests");
        }
    }
}
