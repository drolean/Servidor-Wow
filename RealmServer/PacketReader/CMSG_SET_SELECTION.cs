using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SET_SELECTION : Common.Network.PacketReader
    {
        public ulong PlayerUid;

        public CMSG_SET_SELECTION(byte[] data) : base(data)
        {
            PlayerUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SET_SELECTION] PlayerUid: {PlayerUid}");
#endif
        }
    }
}