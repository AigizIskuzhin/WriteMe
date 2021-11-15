using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Website.Controllers.Rules;
using Website.Infrastructure.Services.Interfaces;
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
            if (chatsViewModel.ChatId != 0) return GetChat(chatsViewModel.ChatId);

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

            var chat = MessengerService.GetChat(id);
            var receiver = chat.GetReceiverChatParticipant(GetConnectedUserID);
            return View("Base/ChatView",
                new ChatViewModel
                {
                    Id = chat.Id,
                    History = chat.GetHistory(), 
                    ConnectedUserId = GetConnectedUserID,
                    IsPrivateChat = chat.IsPrivateChat,
                    ReceiverName = receiver.User.Name,
                    ReceiverId = receiver.Id,
                    IsReceiverOnline = SignalRService.Connections.GetConnections(receiver.User.Id.ToString()).Any(),
                    ReceiverAvatarPath = receiver.User.AvatarPath
                });
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
            var chat = MessengerService.GetChat(chatId);
            var receiver = chat.GetReceiverChatParticipant(GetConnectedUserID);
            return View("PrivateChat/PrivateChatHistoryView",
                new ChatViewModel
                {
                    IsPrivateChat = chat.IsPrivateChat,
                    History = newMessages,
                    ConnectedUserId = GetConnectedUserID,
                    ReceiverName = receiver.User.Name,
                    ReceiverId = receiver.Id,
                    IsReceiverOnline = SignalRService.Connections.GetConnections(receiver.User.Id.ToString()).Any()
                });
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
