using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Website.Infrastructure.Extensions;
using Website.Infrastructure.Services.Interfaces;

namespace Website.Infrastructure.SignalRHubs
{
    public class AppHub : Hub
    {
        private readonly ISignalRService SignalRService;

        private static readonly ConnectionMapping<string> Connections = new ();

        public AppHub(ISignalRService signalRService)
        {
            SignalRService = signalRService;
        }

        public IEnumerable<string> GetConnections(string key) => Connections.GetConnections(key);
        public async Task NotifyAboutNewMessage(string userId)
        {
            foreach (var connection in GetConnections(userId))
                await Clients.Client(connection).SendAsync("NotificationNewMessage");
        }


        public override Task OnConnectedAsync()
        {
            var connectedUserId = Context.GetConnectedUserId();
            var connectionId = Context.ConnectionId;
            
            SignalRService.UserJoining(connectedUserId);
            SignalRService.Connections.Add(connectedUserId, connectionId);

            Connections.Add(connectedUserId, connectionId);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var connectedUserId = Context.GetConnectedUserId();
            var connectionId = Context.ConnectionId;

            
            SignalRService.UserLeaving(connectedUserId);
            
            SignalRService.Connections.Remove(connectedUserId, connectionId);
            Connections.Remove(connectedUserId, connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}
