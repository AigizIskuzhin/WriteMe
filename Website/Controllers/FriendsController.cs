using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class FriendsController : Controller
    {
        public IActionResult Friends(int id)
        {
            return View();
        }

        public IActionResult IncomingFriendRequests()
        {
            return View();
        }

        public IActionResult OutgoingFriendRequests()
        {
            return View();
        }
    }
}
