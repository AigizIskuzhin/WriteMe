using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Website.Controllers.Rules;
using Website.Infrastructure.Extensions;
using Website.ViewModels;
using Website.ViewModels.Profile;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class NewsController : Controller
    {
        private readonly IPostingService PostingService;

        public NewsController(IPostingService postingService)
        {
            PostingService = postingService;
        }

        [Route("/news")]
        public IActionResult News(NewsViewModel newsViewModel)
        {
            newsViewModel.NewsPosts = PostingService.GetSystemPosts();
            newsViewModel.IsAdmin = PostingService.IsAdmin(int.Parse(HttpContext.GetConnectedUserId()));
            ViewData.Add("IsOwner",newsViewModel.IsAdmin);
            return View(newsViewModel);
        }

        public IActionResult GetPostsWithFilter(string text)
        {
            return View("PostsView", PostingService.GetSystemPostsWithFilter(text));
        }

        public IActionResult UploadPost(SystemPostViewModel post)
        {
            PostingService.UploadPost(post);
            return RedirectToAction("News");
        }
        public IActionResult RemovePost(int id)
        {
            if (PostingService.IsAdmin(int.Parse(HttpContext.GetConnectedUserId())))
                PostingService.RemovePost(id);
            return RedirectToAction("News");
        }
    }
}
