using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Infrastructure.Services.Interfaces
{
    public interface ISignalRService
    {
        public event EventHandler<EventArgs<string>> UserJoin;
        
        public event EventHandler<EventArgs<string>> UserLeft;
        public IEnumerable<string> GetConnections(string key);
        public Task NotifyAboutNewMessage(string userId);
    }
}
