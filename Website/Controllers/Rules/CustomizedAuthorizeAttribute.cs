using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Website.Controllers.Rules
{
    public class CustomizedAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userIdentity = context.HttpContext.User.Identity;
            if (userIdentity is { IsAuthenticated: false }) context.Result = new RedirectResult("/authwarn");
            //if (context.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    //all good, add some code if you want. Or don't
            //}
            //else
            //{
            //    //DENIED!
            //    //return "ChallengeResult" to redirect to login page (for example)
            //}
        }
    }
}
