﻿using System.Threading.Tasks;
using SignalR.Hubs;

namespace SignalR
{
    public interface IConnectionManager
    {
        dynamic GetClients<T>() where T : IHub;
        IConnection GetConnection<T>() where T : PersistentConnection;
        Task CloseConnections(string scope);
    }
}
