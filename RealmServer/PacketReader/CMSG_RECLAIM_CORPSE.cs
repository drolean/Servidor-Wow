using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_RECLAIM_CORPSE : Common.Network.PacketReader
    {
        public UInt64 PlayerUid;

        public CMSG_RECLAIM_CORPSE(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_RECLAIM_CORPSE] PlayerUid: {PlayerUid}");
#endif
        }
    }
}