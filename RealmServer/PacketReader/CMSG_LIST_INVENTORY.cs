using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_LIST_INVENTORY : Common.Network.PacketReader
    {
        public ulong Uid;

        public CMSG_LIST_INVENTORY(byte[] data) : base(data)
        {
            Uid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_LIST_INVENTORY] Uid: {Uid}");
#endif
        }
    }
}
