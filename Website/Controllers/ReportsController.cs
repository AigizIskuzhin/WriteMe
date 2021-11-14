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

        public IActionResult CloseReportAndDeletePost(int reportId)
        {
            if(reportId==0)
                return View("Reports", PostingService.GetPostsReports());
            PostingService.CloseReportAndDeletePost(reportId);
            return View("Reports", PostingService.GetPostsReports());
        }

        public IActionResult CloseReport(int reportId)
        {
            if(reportId==0)
                return View("Reports", PostingService.GetPostsReports());
            PostingService.CloseReport(reportId);
            return View("Reports", PostingService.GetPostsReports());
        }
    }
}
