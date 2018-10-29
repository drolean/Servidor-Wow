using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUCTION_LIST_BIDDER_ITEMS : Common.Network.PacketReader
    {
        public ulong AuctioneerId;
        public uint OutbiddedCount;

        public CMSG_AUCTION_LIST_BIDDER_ITEMS(byte[] data) : base(data)
        {
            AuctioneerId = ReadUInt64();
            OutbiddedCount = ReadUInt32();

            for (var i = 0; i < OutbiddedCount; i++)
            {
                //ReadUInt32(); //auction id
            }
#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_AUCTION_LIST_BIDDER_ITEMS] AuctioneerId: {AuctioneerId} OutbiddedCount: {OutbiddedCount}");
#endif
        }
    }
}
