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
            var t = dialogsViewModel.Chats.First().History;
            return View(dialogsViewModel);
        }
        #endregion

        #region Chat

        /// <summary>
        /// Возврат чата по указанному ид, подключенного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Chat(int id)
        {
            var chat = MessengerService.GetChat(id);
            return View(chat);
        } 
        #endregion
    }
}
