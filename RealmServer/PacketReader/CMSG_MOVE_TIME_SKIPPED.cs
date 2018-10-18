using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_MOVE_TIME_SKIPPED : Common.Network.PacketReader
    {
        public uint Lag;
        public ulong PlayerUid;

        public CMSG_MOVE_TIME_SKIPPED(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();
            Lag = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_MOVE_TIME_SKIPPED] PlayerUid: {PlayerUid} Lag: {Lag}");
#endif
        }
    }
}