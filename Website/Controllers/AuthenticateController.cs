using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;
using Website.ViewModels.Base;
using WriteMe.Database.DAL.Entities;

namespace Website.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService AuthenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            AuthenticateService = authenticateService;
        }
        
        [Route("reg")]
        [Route("auth")]
        public async Task<IActionResult> ConfirmMail(ConfirmMailViewModel confirmMailViewModel)
        {
            var t = Request.Path.Value;
            bool isAuth = t!.Contains("auth");
            if (confirmMailViewModel.IsAuth is null)
                return View(new ConfirmMailViewModel { IsAuth = isAuth });

            if (ModelState.IsValid)
            {
                var user = await AuthenticateService.IsUserExistAsync(confirmMailViewModel.MailAddress);
                if (confirmMailViewModel.IsAuth.Value)
                {
                    if (user!=null)
                        return View("EnterPassword", new AuthorizationViewModel
                        {
                            MailAddress = confirmMailViewModel.MailAddress,
                            UserTitle = user.Name + " " + user.Surname[0] +"."
                        });
                    ModelState.AddModelError(nameof(confirmMailViewModel.MailAddress), "Неверный адрес");
                }
                else
                {
                    if (user==null)
                        return View("EnterUserInfo", new RegistrationViewModel
                        {
                            MailAddress = confirmMailViewModel.MailAddress
                        });
                    ModelState.AddModelError(nameof(confirmMailViewModel.MailAddress), "Адрес занят");
                }
            }
            return View(confirmMailViewModel);
        }
        [Route("auth/log")]
        public async Task<IActionResult> EnterPassword(AuthorizationViewModel authorizationViewModel)
        {
            if (authorizationViewModel.MailAddress is null) return RedirectToAction("ConfirmMail");
            if (ModelState.IsValid)
            {
                var user = await AuthenticateService.ConfirmUserAsync(authorizationViewModel.MailAddress,
                    authorizationViewModel.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return Redirect("/app/welcome");
                }
                
                authorizationViewModel.Password = string.Empty;
                ModelState.AddModelError(nameof(authorizationViewModel.Password), "Неверный пароль");
            }
            return View(authorizationViewModel);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.Surname+" "+user.Name+" "+user.Patronymic),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
                new("id",user.Id.ToString())
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
        [Route("exit")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "About");
        }

        public async Task<bool> IsMailExist(string mailAddress) =>
            await AuthenticateService.IsUserExistAsync(mailAddress)!=null;

        [Route("/auth/reg")]
        public async Task<IActionResult> EnterUserInfo(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await AuthenticateService.RegisterAsync(registrationViewModel);
                if (user != null)
                {
                    await Authenticate(user);
                    return Redirect("/app/welcome");
                }

            }
            return View();
        }

        [Route("/authwarn")]
        public IActionResult AuthWarning() => View();

    }
}
