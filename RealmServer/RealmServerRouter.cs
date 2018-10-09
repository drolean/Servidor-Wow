using System.Collections.Generic;
using Common.Globals;
using Common.Helpers;

namespace RealmServer
{
    public class RealmServerRouter
    {
        public delegate void ProcessRealmPacketCallback(RealmServerSession session, byte[] data);

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
                RealmServerSession.DumpPacket(data, realmServerSession);
            }
        }
    }
}