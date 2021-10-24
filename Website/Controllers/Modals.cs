using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class ModalsController : Controller
    {
        public IActionResult CreatePostModalForm() => PartialView();
    }
}
