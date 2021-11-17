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

        public IActionResult Users() => View(UsersService.GetUsersPreviews());
    }
}
