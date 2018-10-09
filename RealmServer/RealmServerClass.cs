using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Common.Helpers;

namespace RealmServer
{
    public class RealmServerClass : IDisposable
    {
        public static List<RealmServerSession> Sessions = new List<RealmServerSession>();
        private readonly Socket _socketHandler;

        public RealmServerClass(IPEndPoint authPoint)
        {
            ActiveConnections = new Dictionary<int, RealmServerSession>();
            _socketHandler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketHandler.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            _socketHandler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.DontLinger, true);
            //_socketHandler.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

            _socketHandler.SendTimeout = 3500;
            _socketHandler.ReceiveTimeout = 3500;
            try
            {
                _socketHandler.Bind(new IPEndPoint(authPoint.Address, authPoint.Port));
                _socketHandler.Listen(100);
                _socketHandler.BeginAccept(ConnectionRequest, _socketHandler);
                Log.Print(LogType.RealmServer, $"Server is now listening at {authPoint.Address}:{authPoint.Port}");
            }
            catch (Exception e)
            {
                var trace = new StackTrace(e, true);
                Log.Print(LogType.Error,
                    $"{e.Message}: {e.Source}\n{trace.GetFrame(trace.FrameCount - 1).GetFileName()}:{trace.GetFrame(trace.FrameCount - 1).GetFileLineNumber()}");
            }
        }

        public Dictionary<int, RealmServerSession> ActiveConnections { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        ///     Dispose
        /// </summary>
        public void Dispose()
        {
            _socketHandler.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <todo>implement logic to check banned IP</todo>
        private void ConnectionRequest(IAsyncResult asyncResult)
        {
            var connectionSocket = ((Socket) asyncResult.AsyncState).EndAccept(asyncResult);
            var connectionId = GetFreeId();

            var session = new RealmServerSession(connectionId, connectionSocket);
            RealmServerSession.Sessions.Add(session);
            ActiveConnections.Add(connectionId, session);
            _socketHandler.BeginAccept(ConnectionRequest, _socketHandler);
        }

        /// <summary>
        ///     Checks if there is a slot available to connect.
        /// </summary>
        /// <returns>int</returns>
        private int GetFreeId()
        {
            for (var i = 0; i < 3500; i++)
                if (!ActiveConnections.ContainsKey(i))
                    return i;

            Log.Print(LogType.Error, "Couldn't find free ID");
            return 0;
        }
    }
}