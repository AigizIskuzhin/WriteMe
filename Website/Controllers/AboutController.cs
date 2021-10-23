using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index() => View();
    }
}
