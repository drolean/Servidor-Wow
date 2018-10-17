using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_SELL_ITEM : Common.Network.PacketReader
    {
        public UInt64 VendorUid;
        public UInt64 ItemUid;
        public byte Count;

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