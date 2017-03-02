using System;
using System.Collections.Generic;
using Common.Globals;
using Common.Helpers;

namespace RealmServer
{
    internal class RealmServerRouter
    {
        public delegate void ProcessLoginPacketCallback(RealmServerSession session, byte[] data);
        public delegate void ProcessLoginPacketCallbackTypes<in T>(RealmServerSession session, T handler);
        public static readonly Dictionary<RealmCMD, ProcessLoginPacketCallback> MCallbacks = new Dictionary<RealmCMD, ProcessLoginPacketCallback>();

        internal static void AddHandler(RealmCMD opcode, ProcessLoginPacketCallback handler)
        {
            MCallbacks.Add(opcode, handler);
        }

        internal static void AddHandler<T>(RealmCMD opcode, ProcessLoginPacketCallbackTypes<T> callback)
        {
            AddHandler(opcode, (session, data) =>
            {
                T generatedHandler = (T)Activator.CreateInstance(typeof(T), data);
                callback(session, generatedHandler);
            });
        }

        internal static void CallHandler(RealmServerSession authServerSession, RealmCMD opcode, byte[] data)
        {
            if (MCallbacks.ContainsKey(opcode))
            {
                MCallbacks[opcode](authServerSession, data);
            }
            else
            {
                Log.Print(LogType.RealmServer, $"Missing handler: {opcode}");
                RealmServerSession.DumpPacket(data, authServerSession);
            }
        }
    }
}