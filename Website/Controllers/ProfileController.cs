using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService ProfileService;
        private int GetConnectedUserID => int.Parse(User.Claims.First(claim => claim.Type.Equals("id")).Value);

        public ProfileController(IProfileService profileService)
        {
            ProfileService = profileService;
        }
        [Route("/profile")]
        public async Task<IActionResult> Profile(int id)
        {
            id = id == 0 ? GetConnectedUserID : id;
            var user = await ProfileService.GetUserAsync(id);
            if (user == null) return RedirectToAction("Profile");
            return View(new ProfileViewModel
            {
                User=user,
                IsOwner = id==GetConnectedUserID,
                UserPosts = ProfileService.GetUserPosts(id)
            });
        }

        [HttpPost]
        public ActionResult UploadAvatar(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {

            }
            return View("Profile");
        }

        public ActionResult SearchUserPosts(int id, string filterText)
        {
            id = id == 0 ? GetConnectedUserID : id;
            return View("PostsView", string.IsNullOrWhiteSpace(filterText) ? ProfileService.GetUserPosts(id) : ProfileService.GetUserPostsWithFilter(id,filterText));
        }
    }
}
