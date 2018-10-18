using System;
using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUCTION_LIST_OWNER_ITEMS : Common.Network.PacketReader
    {
        public UInt64 AuctioneerId;

        public CMSG_AUCTION_LIST_OWNER_ITEMS(byte[] data) : base(data)
        {
            AuctioneerId = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUCTION_LIST_OWNER_ITEMS] AuctioneerId: {AuctioneerId}");
#endif
        }
    }
}