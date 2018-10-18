using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SUMMON_RESPONSE : Common.Network.PacketReader
    {
        public UInt64 TargetUid;
        public bool Result;

        public CMSG_SUMMON_RESPONSE(byte[] data) : base(data)
        {
            TargetUid = ReadUInt64();
            Result = ReadBoolean();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SUMMON_RESPONSE] TargetUid: {TargetUid} Result: {Result}");
#endif
        }
    }
}