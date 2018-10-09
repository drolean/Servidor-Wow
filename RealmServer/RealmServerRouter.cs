using System;
using System.Collections.Generic;
using Common.Globals;
using Common.Helpers;

namespace RealmServer
{
    public class RealmServerRouter
    {
        public delegate void ProcessRealmPacketCallback(RealmServerSession session, byte[] data);

        public delegate void ProcessRealmPacketCallback<in T>(RealmServerSession session, T handler);

        public static readonly Dictionary<RealmEnums, ProcessRealmPacketCallback> MCallbacks =
            new Dictionary<RealmEnums, ProcessRealmPacketCallback>();

        /// <summary>
        ///     Calls the handler.
        /// </summary>
        /// <param name="realmServerSession"></param>
        /// <param name="opcode">The event handler.</param>
        /// <param name="data"></param>
        internal static void CallHandler(RealmServerSession realmServerSession, RealmEnums opcode, byte[] data)
        {
            if (MCallbacks.ContainsKey(opcode))
            {
                MCallbacks[opcode](realmServerSession, data);
            }
            else
            {
                Log.Print(LogType.RealmServer, $"Missing handler: {opcode}");
                Utils.DumpPacket(data);
            }
        }

        internal static void AddHandler(RealmEnums opcode, ProcessRealmPacketCallback handler)
        {
            MCallbacks.Add(opcode, handler);
        }

        internal static void AddHandler<T>(RealmEnums opcode, ProcessRealmPacketCallback<T> callback)
        {
            AddHandler(opcode, (session, data) =>
            {
                var generatedHandler = (T) Activator.CreateInstance(typeof(T), data);
                callback(session, generatedHandler);
            });
        }
    }
}