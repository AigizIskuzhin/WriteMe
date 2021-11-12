using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class GuideController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authorization() => View();
    }
}
