using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Website.Controllers.Rules;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class UsersController : Controller
    {
        private readonly IUsersService UsersService;

        public UsersController(IUsersService usersService)
        {
            UsersService = usersService;
        }
        [Route("/users")]
        public IActionResult Users() => View(UsersService.GetUsersPreviews());

        public IActionResult SearchUsers(string filter) =>
            View("UsersView", string.IsNullOrWhiteSpace(filter)?UsersService.GetUsersPreviews():UsersService.GetUsersPreviews(filter));
    }
}
