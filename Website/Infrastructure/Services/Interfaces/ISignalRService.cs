using System;
using Website.Infrastructure.SignalRHubs;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface ISignalRService
    {
        public event EventHandler<EventArgs<string>> UserJoin;
        
        public event EventHandler<EventArgs<string>> UserLeft;

        public ConnectionMapping<string> Connections { get; }
        public void UserJoining(string id);
        public void UserLeaving(string id);
    }
}
