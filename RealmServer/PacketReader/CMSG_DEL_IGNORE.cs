using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_DEL_IGNORE : Common.Network.PacketReader
    {
        public ulong PlayerUid;

        public CMSG_DEL_IGNORE(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_DEL_IGNORE] PlayerUid: {PlayerUid}");
#endif
        }
    }
}