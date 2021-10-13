using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    [Route("/auth")]
    public class AuthorizationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
