using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Website.Controllers.Rules
{
    public class CustomizedAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdentity = context.HttpContext.User.Identity;
            if (userIdentity is { IsAuthenticated: false }) context.Result = new RedirectToActionResult("AuthWarning","Authenticate",null);
        }
    }
}
