using System.Collections.Generic;
using System.Linq;

namespace Website.Infrastructure.SignalRHubs
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> Connections = new();

        public int Count => Connections.Count;

        public void Add(T key, string connectionId)
        {
            lock (Connections)
            {
                if (!Connections.TryGetValue(key, out var connections))
                {
                    connections = new HashSet<string>();
                    Connections.Add(key, connections);
                }
                lock (connections)
                    connections.Add(connectionId);
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            lock (Connections)
                if (Connections.TryGetValue(key, out var connections))
                    return connections;
            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (Connections)
            {
                if (!Connections.TryGetValue(key, out var connections))
                    return;
                lock (connections)
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                        Connections.Remove(key);
                    
                }
            }
        }
    }
}