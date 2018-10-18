using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_PAGE_TEXT_QUERY represents a packet sent by the client when it wants to retrieve multiple item information.
    /// </summary>
    public sealed class CMSG_PAGE_TEXT_QUERY : Common.Network.PacketReader
    {
        public ulong ItemUid;
        public int PageId;

        public CMSG_PAGE_TEXT_QUERY(byte[] data) : base(data)
        {
            PageId = ReadInt32();
            ItemUid = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_PAGE_TEXT_QUERY] PageId: {PageId} ItemUid: {ItemUid}");
#endif
        }
    }
}