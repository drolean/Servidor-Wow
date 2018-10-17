using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BUY_ITEM_IN_SLOT : Common.Network.PacketReader
    {
        public UInt64 VendorUid;
        public int ItemId;
        public UInt64 ClientUid;
        public byte Slot;
        public byte Count;
        
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