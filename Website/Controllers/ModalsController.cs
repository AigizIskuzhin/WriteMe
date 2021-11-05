using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;
using Website.ViewModels.Profile;

namespace Website.Controllers
{
    public class ModalsController : Controller
    {
        private readonly IPostingService PostingService;

        public ModalsController(IPostingService postingService)
        {
            PostingService = postingService;
        }
        
        [CustomizedAuthorize]
        public IActionResult CreatePostModalForm(PostViewModel post) => PartialView(post);
        [Route("/working")]
        public IActionResult WorkingON() => View();

        public IActionResult AlreadyLoggedIn() => View("AlreadyLoggedInWarning");
        
        [CustomizedAuthorize]
        public IActionResult SendReportModal(int postId)
        {
            var model = new PostReportViewModel
            {
                PostId = postId,
                ReportTypes = PostingService.GetReportTypes()
            };

            return View(model);
        }
    }
}
