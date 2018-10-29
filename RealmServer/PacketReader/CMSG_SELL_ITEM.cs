using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SELL_ITEM : Common.Network.PacketReader
    {
        public byte Count;
        public ulong ItemUid;
        public ulong VendorUid;

        public CMSG_SELL_ITEM(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();
            ItemUid = ReadUInt64();
            Count = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_SELL_ITEM] VendorUid: {VendorUid} ItemUid: {ItemUid} Count: {Count}");
#endif
        }
    }
}
