using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Security.Claims;

namespace Website.Infrastructure.Extensions
{
    public static class GetConnectedUserIdExtension
    {
        public static string GetConnectedUserId(this HubCallerContext context) => context.User!.Claims
            .First(claim => claim.Type.Equals("id")).Value;

        public static string GetConnectedUserId(this ClaimsPrincipal user) => user.Claims
            .First(claim => claim.Type.Equals("id")).Value;

        public static string GetConnectedUserId(this HttpContext context) => context.User.Claims
            .First(claim => claim.Type.Equals("id")).Value;
        
    }
}
