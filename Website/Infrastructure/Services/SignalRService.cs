using System;
using Website.Infrastructure.Services.Interfaces;
using Website.Infrastructure.SignalRHubs;

namespace Website.Infrastructure.Services
{
    public class SignalRService : ISignalRService
    {
        public event EventHandler<EventArgs<string>> UserJoin;
        public event EventHandler<EventArgs<string>> UserLeft;
        public ConnectionMapping<string> Connections { get; } = new();
        public void UserJoining(string id) => UserJoin?.Invoke(this, id);
        public void UserLeaving(string id) => UserLeft?.Invoke(this, id);
    }
}
