using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUCTION_REMOVE_ITEM : Common.Network.PacketReader
    {
        public ulong AuctioneerId;
        public uint AuctionId;

        public CMSG_AUCTION_REMOVE_ITEM(byte[] data) : base(data)
        {
            AuctioneerId = ReadUInt64();
            AuctionId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUCTION_REMOVE_ITEM] AuctioneerId: {AuctioneerId} AuctionId: {AuctionId}");
#endif
        }
    }
}
