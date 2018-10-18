using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_REQUEST_PARTY_MEMBER_STATS : Common.Network.PacketReader
    {
        public UInt64 TargetUid;

        public CMSG_REQUEST_PARTY_MEMBER_STATS(byte[] data) : base(data)
        {
            TargetUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_REQUEST_PARTY_MEMBER_STATS] TargetUid: {TargetUid}");
#endif
        }
    }
}