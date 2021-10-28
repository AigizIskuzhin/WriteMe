using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;

namespace Website.Controllers
{
    [CustomizedAuthorize]
    public class MessengerController : Controller
    {
        #region Get connected user id
        /// <summary>
        /// Получить подключенного пользователя с помощью claims
        /// </summary>
        private int GetConnectedUserID => int.Parse(User.Claims.First(claim => claim.Type.Equals("id")).Value); 
        #endregion

        private readonly IMessengerService MessengerService;

        #region ctor
        public MessengerController(IMessengerService messengerService)
        {
            MessengerService = messengerService;
        } 
        #endregion

        #region Dialogs
        /// <summary>
        /// Открытие страницы диалогов, возврат диалого подключенного пользователя
        /// </summary>
        /// <param name="dialogsViewModel"></param>
        /// <returns></returns>
        public IActionResult Dialogs(DialogsViewModel dialogsViewModel)
        {
            int connectedUserId = GetConnectedUserID;
            dialogsViewModel.Chats = MessengerService.GetUserChats(connectedUserId);
            return View(dialogsViewModel);
        }
        #endregion

        #region GetChatWithUser

        /// <summary>
        /// Возврат чата с указанным пользователем, подключенного пользователя
        /// </summary>
        /// <param name="dialogsViewModel"></param>
        /// <returns></returns>
        public IActionResult GetChatWithUser(DialogsViewModel dialogsViewModel)
        {
            if (dialogsViewModel.TargetedUserId == 0)
                return RedirectToAction("Dialogs");
            var chat = MessengerService.GetPrivateChatWithUser(dialogsViewModel.TargetedUserId, GetConnectedUserID);
            if (chat is null)
                chat = MessengerService.GetNewChatWithUser(dialogsViewModel.TargetedUserId, GetConnectedUserID);
            if (chat is null)
                return RedirectToAction("Dialogs");
            return View("ChatWithUser", chat);
        }
        #endregion

        public IActionResult GetGroupChat(DialogsViewModel dialogsViewModel)
        {
            if (dialogsViewModel.TargetedGroupChatId == 0)
                return RedirectToAction("Dialogs");
            return View("GroupChat");
        }

    }
}
