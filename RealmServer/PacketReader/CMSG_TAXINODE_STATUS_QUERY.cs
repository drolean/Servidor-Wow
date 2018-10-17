using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_TAXINODE_STATUS_QUERY : Common.Network.PacketReader
    {
        public UInt64 Uid;

        public CMSG_TAXINODE_STATUS_QUERY(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_TAXINODE_STATUS_QUERY] Uid: {Uid}");
#endif
        }
    }
}