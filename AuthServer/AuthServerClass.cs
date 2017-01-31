using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Common.Helpers;

namespace AuthServer
{
    internal class AuthServerClass : IDisposable
    {
        private readonly Socket _socketHandler;
        public Dictionary<int, AuthServerSession> ActiveConnections { get; protected set; }

        private int ConnectionsCount => ActiveConnections.Count;

        public AuthServerClass(IPEndPoint authPoint)
        {
            ActiveConnections = new Dictionary<int, AuthServerSession>();
            _socketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketHandler.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            _socketHandler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
            _socketHandler.SendTimeout = 3500;
            _socketHandler.ReceiveTimeout = 3500;
            try
            {
                _socketHandler.Bind(new IPEndPoint(authPoint.Address, authPoint.Port));
                _socketHandler.Listen(100);
                _socketHandler.BeginAccept(ConnectionRequest, _socketHandler);
                Log.Print(LogType.AuthServer, $"Server is now listening at {authPoint.Address}:{authPoint.Port}");
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error, $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }

        private void ConnectionRequest(IAsyncResult asyncResult)
        {
            Socket connectionSocket = ((Socket)asyncResult.AsyncState).EndAccept(asyncResult);
            int connectionId = GetFreeId();
            ActiveConnections.Add(connectionId, new AuthServerSession(connectionId, connectionSocket));
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