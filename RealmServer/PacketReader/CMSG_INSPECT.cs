using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_INSPECT : Common.Network.PacketReader
    {
        public ulong PlayerUid;

        public CMSG_INSPECT(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_INSPECT] PlayerUid: {PlayerUid}");
#endif
        }
    }
}
