using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Website.Controllers.Rules;
using Website.ViewModels;
using Website.ViewModels.Profile;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class ModalsController : Controller
    {
        private readonly IPostingService PostingService;

        public ModalsController(IPostingService postingService)
        {
            PostingService = postingService;
        }
        
        public IActionResult CreatePost(PostViewModel post) => View(post);
        
        public IActionResult EditPost(PostViewModel post) => View(post);

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

        [CustomizedAuthorize]
        public IActionResult UploadAvatarModal() => View("UploadAvatar");
    }
}
