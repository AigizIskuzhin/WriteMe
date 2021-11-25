using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Website.Infrastructure.SignalRHubs
{
    public class SignalRService : ConnectionMapping<string>,ISignalRService
    {
        private readonly IHubContext<AppHub> AppHub;

        public SignalRService(IHubContext<AppHub> appHub)
        {
            AppHub = appHub;
        }

        public event EventHandler<EventArgs<string>> UserJoin;
        public event EventHandler<EventArgs<string>> UserLeft;
        public event EventHandler<EventArgs<PrivateNotificationMessage>> NewMessage;
        public ConnectionMapping<string> Connections { get; } = new();
        public void UserJoining(string id, string connectionId)
        {
            Connections.Add(id, connectionId);
            UserJoin?.Invoke(this, id);
        }

        public void UserLeaving(string id, string connectionId)
        {
            Connections.Remove(id, connectionId);
            UserLeft?.Invoke(this, id);
        }

        public async Task NotifyUserFromPrivateChatAboutNewMessage(string chatId, string senderId)
        {
            //var receivers = MessengerService.GetChatParticipantIds(int.Parse(chatId)).Where(r=>r!=senderId);
            //foreach (var receiver in receivers)
            //    foreach (var connection in Connections.GetConnections(receiver))  
            //        await AppHub.Clients.Client(connection).SendAsync("NotifyAboutNewMessage", chatId);
        }

    }
}
