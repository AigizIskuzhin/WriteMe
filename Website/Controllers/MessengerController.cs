using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Website.Controllers.Rules;
using Website.Infrastructure.SignalRHubs;
using Website.ViewModels;
using Website.ViewModels.Messenger;

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
        private readonly ISignalRService SignalRService;

        #region ctor
        public MessengerController(IMessengerService messengerService, ISignalRService signalRService)
        {
            MessengerService = messengerService;
            SignalRService = signalRService;
        } 
        #endregion

        #region Chats

        /// <summary>
        /// Открытие страницы чатов, возвращает чаты подключенного пользователя
        /// </summary>
        /// <param name="chatsViewModel"></param>   
        /// <returns></returns>
        public IActionResult Chats(ChatsViewModel chatsViewModel)
        {
            if (chatsViewModel.id != 0) return GetChat(chatsViewModel.id);

            int connectedUserId = GetConnectedUserID;
            chatsViewModel.ChatsPreviews = MessengerService.GetUserChatsPreviews(connectedUserId);
            return View(chatsViewModel);
        }
        #endregion

        #region GetChatWithUser

        public IActionResult GetChat(int id)
        {
            if (id== 0)
                return RedirectToAction("Chats");

            var chat = MessengerService.GetChat(id, GetConnectedUserID);
            return View("Base/ChatView",chat);
        }
        /// <summary>
        /// Возврат чата с указанным пользователем, подключенного пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// GetPrivateChatWithUser?idId=id
        public IActionResult GetPrivateChatWithUser(int id)
        {
            if (id== 0)
                return RedirectToAction("Chats");

            var chat = MessengerService.GetPrivateChatWithUser(id, GetConnectedUserID);
            if (chat is null)
                return RedirectToAction("Chats");
            ViewData["UserId"] = GetConnectedUserID;
            return RedirectPermanent("/messenger/chats?id="+chat.Id);
        }
        #endregion

        #region SendMessageToChat
        public async Task SendMessageToChat(int chatId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;
            await MessengerService.SendMessageToChat(GetConnectedUserID, chatId, text);
        }
        #endregion

        #region GetNewMessages

        public IActionResult GetNewMessages(int chatId, int lastMessageId)
        {
            var newMessages = MessengerService.GetNewMessagesFromLast(chatId, lastMessageId);
            var chat = MessengerService.GetChat(chatId, GetConnectedUserID);
            chat.History = newMessages;
            return View("PrivateChat/PrivateChatHistoryView", chat);
        }

        #endregion

        //public IActionResult GetGroupChat(ChatsViewModel chatsViewModel)
        //{
        //    if (chatsViewModel.TargetedGroupChatId == 0)
        //        return RedirectToAction("Dialogs");
        //    return View("GroupChat");
        //}

    }
}
