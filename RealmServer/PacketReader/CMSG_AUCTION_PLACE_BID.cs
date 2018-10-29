using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUCTION_PLACE_BID : Common.Network.PacketReader
    {
        public ulong AuctioneerId;
        public uint AuctionId;
        public uint Bid;

        public CMSG_AUCTION_PLACE_BID(byte[] data) : base(data)
        {
            AuctioneerId = ReadUInt64();
            AuctionId = ReadUInt32();
            Bid = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_AUCTION_PLACE_BID] AuctioneerId: {AuctioneerId} AuctionId: {AuctionId} Bid {Bid}");
#endif
        }
    }
}
