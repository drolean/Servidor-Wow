using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_GROUP_ASSISTANT_LEADER : Common.Network.PacketReader
    {
        public ulong TargetUid;

        public CMSG_GROUP_ASSISTANT_LEADER(byte[] data) : base(data)
        {
            TargetUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_GROUP_ASSISTANT_LEADER] TargetUid: {TargetUid}");
#endif
        }
    }
}
