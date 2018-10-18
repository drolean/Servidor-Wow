using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MEETINGSTONE_JOIN : Common.Network.PacketReader
    {
        public UInt64 ObjectUid;

        public CMSG_MEETINGSTONE_JOIN(byte[] data) : base(data)
        {
            ObjectUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_MEETINGSTONE_JOIN] ObjectUid: {ObjectUid}");
#endif
        }
    }
}