using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class ReportsController : Controller
    {
        private readonly IPostingService PostingService;

        public ReportsController(IPostingService postingService)
        {
            PostingService = postingService;
        }

        [Route("/reports")]
        public IActionResult Reports() => View(PostingService.GetPostsReports());
    }
}
