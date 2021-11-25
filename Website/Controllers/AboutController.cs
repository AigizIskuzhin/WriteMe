using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;

namespace Website.Controllers
{
    public class AboutController : Controller
    {
        [RedirectOnJoin]
        public IActionResult Index() => View();
    }
}
