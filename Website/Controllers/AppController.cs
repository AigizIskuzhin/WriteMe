using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;
using WriteMe.Database.DAL.Entities;

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

        public IActionResult Welcome()
        {
            var t = HttpContext;
            
            return View();
        }
        public async Task<IActionResult>  Profile(int? id)
        {
            User user;
            if (id == null)
            {
                var userID = User.Claims.First(claim => claim.Type == "id").Value;
                user = await ProfileService.GetUserAsync(Int32.Parse(userID));
                return View(new ProfileViewModel
                {
                    User=user
                });
            }
            else
            {

                user = await ProfileService.GetUserAsync(Int32.Parse(id.ToString()));
            }
            if (user == null) return RedirectToAction("Profile");
            return View(new ProfileViewModel
            {
                User=user
            });
        }
    }
}
