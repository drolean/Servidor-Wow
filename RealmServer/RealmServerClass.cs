using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Framework.Helpers;

namespace RealmServer
{
    internal class RealmServerClass : IDisposable
    {
        private readonly Socket _socketHandler;
        public Dictionary<int, RealmServerSession> ActiveConnections { get; protected set; }

        private int ConnectionsCount => ActiveConnections.Count;

        public RealmServerClass(IPEndPoint realmPoint)
        {
            ActiveConnections = new Dictionary<int, RealmServerSession>();
            _socketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketHandler.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            _socketHandler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
            _socketHandler.SendTimeout = 3500;
            _socketHandler.ReceiveTimeout = 3500;
            try
            {
                _socketHandler.Bind(new IPEndPoint(realmPoint.Address, realmPoint.Port));
                _socketHandler.Listen(100);
                _socketHandler.BeginAccept(ConnectionRequest, _socketHandler);
                Log.Print(LogType.RealmServer, $"Server is now listening at {realmPoint.Address}:{realmPoint.Port}");
            }
            catch (Exception e)
            {
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}");
            }
        }

        private void ConnectionRequest(IAsyncResult asyncResult)
        {
            Socket connectionSocket = ((Socket)asyncResult.AsyncState).EndAccept(asyncResult);
            int connectionId = GetFreeId();
            ActiveConnections.Add(connectionId, new RealmServerSession(connectionId, connectionSocket));
            _socketHandler.BeginAccept(ConnectionRequest, _socketHandler);
        }

        private int GetFreeId()
        {
            for (int i = 0; i < 3500; i++)
            {
                if (!ActiveConnections.ContainsKey(i))
                    return i;
            }

            Log.Print(LogType.Error, $"Couldn't find free ID");
            return 0;
        }

        public void Dispose()
        {
            _socketHandler.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
