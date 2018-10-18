using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_REPAIR_ITEM : Common.Network.PacketReader
    {
        public UInt64 VendorUid;
        public UInt64 ItemUid;

        public CMSG_REPAIR_ITEM(byte[] data) : base(data)
        {
            VendorUid = ReadUInt64();
            ItemUid = ReadUInt64();
            //var useGuildFunds = packet.ReadBoolean();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_REPAIR_ITEM] VendorUid: {VendorUid} ItemUid: {ItemUid}");
#endif
        }
    }
}