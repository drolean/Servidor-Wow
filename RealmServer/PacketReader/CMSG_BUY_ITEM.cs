using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BUY_ITEM : Common.Network.PacketReader
    {
        public byte Count;
        public int ItemId;
        public byte Slot;
        public ulong VendorUid;

        public CMSG_BUY_ITEM(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();
            ItemId = ReadInt32();
            Count = ReadByte();
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_BUY_ITEM] VendorUid: {VendorUid} ItemId: {ItemId} Count: {Count} Slot: {Slot}");
#endif
        }
    }
}