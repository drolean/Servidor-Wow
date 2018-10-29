using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_SELECTION : Common.Network.PacketReader
    {
        public ulong TargetUid;

        public CMSG_SET_SELECTION(byte[] data) : base(data)
        {
            TargetUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_SELECTION] TargetUid: {TargetUid}");
#endif
        }
    }
}
