using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website.ViewModels.Base;

namespace Website.Controllers
{
    [Route("/auth")]
    public class AuthenticateController : Controller
    {
        [HttpGet]
        [Route("/index")]
        public IActionResult ConfirmMail()
        {
            var requestPath = HttpContext.Request.Path.Value;
            bool isAuth = requestPath.Contains("auth");
            return View(new ConfirmMailViewModel{ IsAuth = isAuth});
        }
    }
}
