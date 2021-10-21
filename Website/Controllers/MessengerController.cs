using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class MessengerController : Controller
    {
        public IActionResult Dialogs()
        {
            return View();
        }

        public IActionResult Chat(int id)
        {
            return View();
        }
    }
}
