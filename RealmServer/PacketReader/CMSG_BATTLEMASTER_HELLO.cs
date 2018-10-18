using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BATTLEMASTER_HELLO : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_BATTLEMASTER_HELLO(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BATTLEMASTER_HELLO] Uid: {Uid}");
#endif
        }
    }
}