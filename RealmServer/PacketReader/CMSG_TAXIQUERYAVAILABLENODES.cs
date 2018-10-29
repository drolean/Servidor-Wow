using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_TAXIQUERYAVAILABLENODES : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_TAXIQUERYAVAILABLENODES(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_TAXIQUERYAVAILABLENODES] Uid: {Uid}");
#endif
        }
    }
}
