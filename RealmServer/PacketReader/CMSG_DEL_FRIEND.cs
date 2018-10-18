using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_DEL_FRIEND : Common.Network.PacketReader
    {
        public ulong PlayerUid;

        public CMSG_DEL_FRIEND(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_DEL_FRIEND] PlayerUid: {PlayerUid}");
#endif
        }
    }
}