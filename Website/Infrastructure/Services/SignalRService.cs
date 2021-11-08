using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Website.Infrastructure.Extensions;
using Website.Infrastructure.Services.Interfaces;
using Website.Infrastructure.SignalRHubs;

namespace Website.Infrastructure.Services
{
    public class SignalRService : Hub, ISignalRService
    {
        public event EventHandler<EventArgs<string>> UserJoin;
        public event EventHandler<EventArgs<string>> UserLeft;

        private static readonly ConnectionMapping<string> Connections = new ();

        public IEnumerable<string> GetConnections(string key) => Connections.GetConnections(key);
        public async Task NotifyAboutNewMessage(string userId)
        {
            foreach (var connection in GetConnections(userId))
                await Clients.Client(connection).SendAsync("NotificationNewMessage");
        }


        public override Task OnConnectedAsync()
        {
            var connectedUserId = Context.GetConnectedUserId();

            UserJoin?.Invoke(this, connectedUserId);


            Connections.Add(Context.GetConnectedUserId(), connectedUserId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectedUserId = Context.GetConnectedUserId();

            UserLeft?.Invoke(this, connectedUserId);

            Connections.Remove(Context.GetConnectedUserId(), connectedUserId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
