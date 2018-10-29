using System;
using System.Collections.Generic;
using Common.Globals;
using Common.Helpers;

namespace AuthServer
{
    internal class AuthServerRouter
    {
        public delegate void ProcessLoginPacketCallback(AuthServerSession session, byte[] data);

        public delegate void ProcessLoginPacketCallbackTypes<in T>(AuthServerSession session, T handler);

        public static readonly Dictionary<AuthCMD, ProcessLoginPacketCallback> MCallbacks =
            new Dictionary<AuthCMD, ProcessLoginPacketCallback>();

        public static void AddHandler(AuthCMD opcode, ProcessLoginPacketCallback handler)
        {
            MCallbacks.Add(opcode, handler);
        }

        public static void AddHandler<T>(AuthCMD opcode, ProcessLoginPacketCallbackTypes<T> callback)
        {
            AddHandler(opcode, (session, data) =>
            {
                var generatedHandler = (T) Activator.CreateInstance(typeof(T), data);
                callback(session, generatedHandler);
            });
        }

        /// <summary>
        ///     Calls the handler.
        /// </summary>
        /// <param name="authServerSession"></param>
        /// <param name="opcode">The event handler.</param>
        /// <param name="data"></param>
        internal static void CallHandler(AuthServerSession authServerSession, AuthCMD opcode, byte[] data)
        {
            if (MCallbacks.ContainsKey(opcode))
            {
                MCallbacks[opcode](authServerSession, data);
            }
            else
            {
                Log.Print(LogType.AuthServer, $"Missing handler: {opcode}");
                AuthServerSession.DumpPacket(data, authServerSession);
            }
        }
    }
}
