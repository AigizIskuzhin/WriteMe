using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class AppController : Controller
    {
        private readonly IProfileService ProfileService;

        public AppController(IProfileService profileService)
        {
            ProfileService = profileService;
        }

        public IActionResult Welcome() => View();

        public async Task<IActionResult>  Profile(int id)
        {
            id = id == 0 ? GetConnectedUserID : id;
            var user = await ProfileService.GetUserAsync(id);
            if (user == null) return RedirectToAction("Profile");
            return View(new ProfileViewModel
            {
                User=user
            });
        }

        private int GetConnectedUserID => int.Parse(User.Claims.First(claim => claim.Type.Equals("id")).Value);
    }
}
