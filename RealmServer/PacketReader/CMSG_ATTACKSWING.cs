using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_ATTACKSWING : Common.Network.PacketReader
    {
        public ulong TargetUid;

        public CMSG_ATTACKSWING(byte[] data) : base(data)
        {
            TargetUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ATTACKSWING] TargetUid: {TargetUid}");
#endif
        }
    }
}
