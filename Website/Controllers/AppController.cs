using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;
using Website.ViewModels.Base;

namespace Website.Controllers
{
    public class AppController : Controller
    {
        private readonly IAuthorizationService AuthorizationService;

        public AppController(IAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }

        [HttpGet]
        [Route("app/auth")]
        [Route("app/reg")]
        public IActionResult ConfirmMail()
        {
            var requestPath = HttpContext.Request.Path.Value;
            bool isAuth = requestPath.Contains("auth");
            return View(new ConfirmMailViewModel{ IsAuth = isAuth});
        }
        
        [Route("app/auth")]
        [Route("app/reg")]
        public async Task<IActionResult>  ConfirmMail(ConfirmMailViewModel confirmMailViewModel)
        {
            if (ModelState.IsValid)
            {
                if (confirmMailViewModel.IsAuth)
                {
                    if(await AuthorizationService.IsUserExistAsync(confirmMailViewModel.MailAddress))
                        return View("EnterPassword", new AuthorizationViewModel 
                            { MailAddress = confirmMailViewModel.MailAddress });
                    ModelState.AddModelError(nameof(confirmMailViewModel.MailAddress),"Неверный адрес");
                }
                else
                {
                    if(!await AuthorizationService.IsUserExistAsync(confirmMailViewModel.MailAddress))
                        return View("EnterUserInfo", new RegistrationViewModel 
                            { MailAddress = confirmMailViewModel.MailAddress });
                    ModelState.AddModelError(nameof(confirmMailViewModel.MailAddress),"Адрес занят");
                }
            }
            return View(confirmMailViewModel);
        }
        [Route("app/auth/log")]
        public async Task<IActionResult> EnterPassword(AuthorizationViewModel authorizationViewModel )
        {
            if (authorizationViewModel.MailAddress is null) return RedirectToAction("AuthWarning");
            if (ModelState.IsValid)
            {
                if (await AuthorizationService.ConfirmUserAsync(authorizationViewModel.MailAddress,authorizationViewModel.Password))
                {
                    await Authenticate(authorizationViewModel.MailAddress);
                    return Redirect("/");
                }
                authorizationViewModel.Password = string.Empty;
                ModelState.AddModelError(nameof(authorizationViewModel.Password), "Неверный пароль");
                return View("ConfirmMail", authorizationViewModel);
            }
            return View("ConfirmMail", authorizationViewModel);;
        }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [Route("exit")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult CheckMail() => View();

        [HttpPost]
        public async Task<IActionResult> CheckMail(ConfirmMailViewModel confirmMailViewModel)
        {
            if (ModelState.IsValid)
            {
                if(await AuthorizationService.IsUserExistAsync(confirmMailViewModel.MailAddress))
                    return View("EnterPassword", new AuthorizationViewModel 
                        { MailAddress = confirmMailViewModel.MailAddress });
                ModelState.AddModelError(nameof(confirmMailViewModel.MailAddress),"Неверный адрес");
            }
            return View(confirmMailViewModel);
        }

        public async Task<bool> IsMailExist(string mailAddress) =>
            await AuthorizationService.IsUserExistAsync(mailAddress);

        public IActionResult EnterUserInfo() => View();

        [Route("/app/authwarn")]
        public IActionResult AuthWarning() => View();

    }
}
