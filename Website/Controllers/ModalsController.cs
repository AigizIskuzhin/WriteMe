using Microsoft.AspNetCore.Mvc;
using Website.ViewModels.Profile;

namespace Website.Controllers
{
    public class ModalsController : Controller
    {
        public IActionResult CreatePostModalForm(PostViewModel post) => PartialView(post);
    }
}
