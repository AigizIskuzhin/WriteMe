using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Database.DAL.Entities;
using Database.DAL.Entities.Base;
using Database.Interfaces;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;

namespace Website.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService AuthenticateService;

        public AuthenticateController(IAuthenticateService authenticateService)
        {
            AuthenticateService = authenticateService;
        }

        
        [OnAuthorizeAttribute]
        [Route("auth")]
        public async Task<IActionResult> ConfirmMailForAuthorization(ConfirmMailViewModel confirmMailViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await AuthenticateService.IsUserExistAsync(confirmMailViewModel.MailAddress);
                
                
                string surname = string.Empty;

                if(user!=null)
                    if (!string.IsNullOrWhiteSpace(user.Surname))
                        surname = " " + user.Surname[0] + ".";

                if (user != null)
                    return View("EnterPassword", new AuthorizationViewModel
                    {
                        MailAddress = confirmMailViewModel.MailAddress,
                        UserTitle = user.Name + surname
                    });
                ModelState.AddModelError(nameof(confirmMailViewModel.MailAddress), "Неверный адрес");
            }
            return View(confirmMailViewModel);
        }
        
        [OnAuthorizeAttribute]
        [Route("reg")]
        public async Task<IActionResult> ConfirmMailForRegistration(ConfirmMailViewModel confirmMailViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await AuthenticateService.IsUserExistAsync(confirmMailViewModel.MailAddress);
                if (user==null)
                    return View("EnterUserInfo", new RegistrationViewModel
                    {
                        MailAddress = confirmMailViewModel.MailAddress
                    });
                ModelState.AddModelError(nameof(confirmMailViewModel.MailAddress), "Адрес занят");
            }
            return View(confirmMailViewModel);
        }

        [Route("auth/log")]
        public async Task<IActionResult> EnterPassword(AuthorizationViewModel authorizationViewModel)
        {
            if (authorizationViewModel.MailAddress is null) return RedirectToAction("ConfirmMailForAuthorization");
            if (ModelState.IsValid)
            {
                var user = await AuthenticateService.ConfirmUserAsync(authorizationViewModel.MailAddress,
                    authorizationViewModel.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return Redirect("/news");
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
        
        [OnAuthorizeAttribute]
        [Route("/auth/reg")]
        public async Task<IActionResult> EnterUserInfo(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                if (!registrationViewModel.Password.Equals(registrationViewModel.ConfirmPassword))
                {
                    ModelState.AddModelError("ConfirmPassword","Пароли не совпадают");
                    return View(registrationViewModel);
                }
                var user = await AuthenticateService.RegisterAsync(registrationViewModel);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Profile","Profile");
                }

            }
            return View();
        }

        [Route("/authwarn")]
        public IActionResult AuthWarning() => View();

    }
}
