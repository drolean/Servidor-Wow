using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_BUYBACK_ITEM : Common.Network.PacketReader
    {
        public UInt64 VendorUid;
        public int SLot;

        public CMSG_BUYBACK_ITEM(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();
            SLot = ReadInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_BUYBACK_ITEM] VendorUid: {VendorUid} SLot: {SLot}");
#endif
        }
    }
}