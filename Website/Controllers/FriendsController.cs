using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels.Friends;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class FriendsController : Controller
    {
        #region Get connected user id
        /// <summary>
        /// Получить подключенного пользователя с помощью claims
        /// </summary>
        private int GetConnectedUserID => int.Parse(User.Claims.First(claim => claim.Type.Equals("id")).Value);
        #endregion
        private IFriendsService FriendsService;
        public FriendsController(IFriendsService friendsService)
        {
            FriendsService = friendsService;
        }
        public string Friends(int id)
        {
            return FriendsService.GetUserFriends(id).ToArray().ToString();
        }

        public IActionResult IncomingFriendRequests()
        {
            return View();
        }

        public IActionResult OutgoingFriendRequests()
        {
            return View();
        }
    }
}
