using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Website.Infrastructure.Extensions;

namespace Website.Infrastructure.SignalRHubs
{
    public class AppHub : Hub
    {
        public readonly ISignalRService SignalRService;

        public AppHub(ISignalRService signalRService)
        {
            SignalRService = signalRService;
            //SignalRService.NewMessage += SignalRService_OnNewMessage;
        }

        //private void SignalRService_OnNewMessage(object sender, EventArgs<PrivateNotificationMessage> e) =>
          //  NotifyUserAboutNewMessage(e.Arg.ReceiverId, e.Arg.ChatId);

        private void NotifyUserAboutNewMessage(string receiverID, string chatId)
        {
            foreach (var connection in SignalRService.Connections.GetConnections(receiverID))
                Clients.Client(connection).SendAsync("NotifyAboutNewMessage", chatId);
        }
        public async Task NotifyAboutNewMessageFromUser(string userId, string senderId, string text)
        {
            foreach (var connection in SignalRService.Connections.GetConnections(userId))
                await Clients.Client(connection).SendAsync("NotificationNewMessage");
        }

        public override Task OnConnectedAsync()
        {
            var connectedUserId = Context.GetConnectedUserId();
            var connectionId = Context.ConnectionId;
            
            SignalRService.UserJoining(connectedUserId,connectionId);
            
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectedUserId = Context.GetConnectedUserId();
            var connectionId = Context.ConnectionId;

            SignalRService.UserLeaving(connectedUserId,connectionId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}
