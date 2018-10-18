using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BUY_STABLE_SLOT : Common.Network.PacketReader
    {
        public UInt64 StableId;

        public CMSG_BUY_STABLE_SLOT(byte[] data) : base(data)
        {
            StableId = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BUY_STABLE_SLOT] StableId: {StableId}");
#endif
        }
    }
}