using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class MSG_AUCTION_HELLO : Common.Network.PacketReader
    {
        public ulong AuctioneerId;

        public MSG_AUCTION_HELLO(byte[] data) : base(data)
        {
            AuctioneerId = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[MSG_AUCTION_HELLO] AuctioneerId: {AuctioneerId}");
#endif
        }
    }
}
