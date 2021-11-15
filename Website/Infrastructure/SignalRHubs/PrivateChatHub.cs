using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Website.Controllers.Rules;
using Website.Infrastructure.Extensions;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.SignalRHubs
{
    [CustomizedAuthorize]
    public class PrivateChatHub : Hub
    {
        #region Get connected user id
        /// <summary>
        /// Получить подключенного пользователя с помощью claims
        /// </summary>
        private string GetConnectedUserID => Context.GetConnectedUserId(); 
        #endregion

        private readonly ISignalRService SignalRService;
        private readonly IMessengerService MessengerService;
        private readonly IHubContext<AppHub> AppHub;

        private string GetChatId => Context.GetHttpContext().Request.Path.Value.Replace("/privatesignalr/","");

        private static readonly ConnectionMapping<string> Connections = new ();
        private static readonly Dictionary<string, List<string>> ChatsToUsers = new();

        public PrivateChatHub(ISignalRService signalRService, IMessengerService messengerService, IHubContext<AppHub> appHub)
        {
            SignalRService = signalRService;
            MessengerService = messengerService;
            AppHub = appHub;

            SignalRService.UserJoin += SignalRServiceOnUserJoin;
            SignalRService.UserLeft += SignalRServiceOnUserLeft;
        }

        private void SignalRServiceOnUserLeft(object sender, EventArgs<string> e)
        {

            if(Connections.GetConnections(e).Any())return;
            
        }

        private void SignalRServiceOnUserJoin(object sender, EventArgs<string> e)
        {
            //Clients.GroupExcept()

        }

        public async Task SendMessage(string text)
        {
            var connectedUserId = GetConnectedUserID;
            var chatId = GetChatId;

            if(string.IsNullOrWhiteSpace(text))return;
            await MessengerService.SendMessageToChat(int.Parse(connectedUserId), int.Parse(chatId), text);
            await NotifyChatAboutNewMessages(chatId);

            // MOVED TO MessengerService
            //var receivers = MessengerService.GetChatParticipantIds(int.Parse(chatId)).Where(r=>r!=connectedUserId);
            //foreach (var receiver in receivers)
            //{
            //    foreach (var connection in SignalRService.Connections.GetConnections(receiver))  
            //        await AppHub.Clients.Client(connection).SendAsync("NotifyAboutNewMessage", chatId);
            //}
        }

        private async Task NotifyChatAboutNewMessages(string chatId) =>
            await Clients.Group(chatId).SendAsync("OnNewMessages");

        public override async Task OnConnectedAsync()
        {
            var connectedUserId = GetConnectedUserID;
            var chatId = GetChatId;

            //await AppHub.Clients.All.SendAsync("NotifyAboutNewMessage", chatId);
            //if (!ChatsToUsers[chatId].Contains(connectedUserId))
            //    ChatsToUsers[chatId].Add(connectedUserId);

            //if (!Connections.GetConnections(connectedUserId).Any())
            //    await Clients.GroupExcept(chatId, Connections.GetConnections(connectedUserId))
            //        .SendAsync("NotifyAboutReceiverJoin");
            //if(SignalRService.GetConnections(connectedUserId).Any())
            //    await Clients.GroupExcept(chatId, Connections.GetConnections(connectedUserId))
            //        .SendAsync("NotifyAboutReceiverOnline");
            Connections.Add(connectedUserId, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectedUserId = GetConnectedUserID;
            var chatId = GetChatId;

            
            if(ChatsToUsers[chatId].Contains(connectedUserId))
                ChatsToUsers[chatId].Remove(connectedUserId);

            Connections.Remove(GetConnectedUserID, Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetChatId);
            //if (!SignalRService.GetConnections(connectedUserId).Any())
            //    await Clients.GroupExcept(chatId, Enumerable.Empty<string>()).SendAsync("NotifyAboutReceiverOffline");
                
            await base.OnDisconnectedAsync(exception);
        }
        
    }
}
