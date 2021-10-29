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
        /// <param name="chatsViewModel"></param>
        /// <returns></returns>
        public IActionResult Chats(ChatsViewModel chatsViewModel)
        {
            int connectedUserId = GetConnectedUserID;
            chatsViewModel.Chats = MessengerService.GetUserChats(connectedUserId);
            return View(chatsViewModel);
        }
        #endregion

        #region GetChatWithUser

        /// <summary>
        /// Возврат чата с указанным пользователем, подключенного пользователя
        /// </summary>
        /// <param name="chatsViewModel"></param>
        /// <returns></returns>
        /// GetChatWithUser?TargetedUserId=id
        public IActionResult GetChatWithUser(ChatsViewModel chatsViewModel)
        {
            if (chatsViewModel.TargetedUserId == 0)
                return RedirectToAction("Chats");
            var chat = MessengerService.GetPrivateChatWithUser(chatsViewModel.TargetedUserId, GetConnectedUserID);
            if (chat is null)
                chat = MessengerService.GetNewChatWithUser(chatsViewModel.TargetedUserId, GetConnectedUserID);
            if (chat is null)
                return RedirectToAction("Chats");
            return View("PrivateChat/PrivateChatView", chat);
        }
        #endregion

        #region SendMessageToPrivateChat
        public IActionResult SendMessageToPrivateChat(ChatsViewModel chatsViewModel)
        {

            return null;
        }
        #endregion

        public IActionResult GetGroupChat(ChatsViewModel chatsViewModel)
        {
            if (chatsViewModel.TargetedGroupChatId == 0)
                return RedirectToAction("Dialogs");
            return View("GroupChat");
        }

    }
}
