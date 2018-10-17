using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BUY_ITEM : Common.Network.PacketReader
    {
        public UInt64 VendorUid;
        public int ItemId;
        public byte Count;
        public byte Slot;

        public CMSG_BUY_ITEM(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();
            ItemId = ReadInt32();
            Count = ReadByte();
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BUY_ITEM] VendorUid: {VendorUid} ItemId: {ItemId} Count: {Count} Slot: {Slot}");
#endif
        }
    }
}