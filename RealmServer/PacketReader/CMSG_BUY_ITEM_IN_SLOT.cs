using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BUY_ITEM_IN_SLOT : Common.Network.PacketReader
    {
        public ulong ClientUid;
        public byte Count;
        public int ItemId;
        public byte Slot;
        public ulong VendorUid;

        public CMSG_BUY_ITEM_IN_SLOT(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();
            ItemId = ReadInt32();
            ClientUid = ReadUInt64();
            Slot = ReadByte();
            Count = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BUY_ITEM_IN_SLOT] VendorUid: {VendorUid} ItemId: {ItemId} " +
                                     $"ClientUid: {ClientUid} Count: {Count} Slot: {Slot}");
#endif
        }
    }
}
