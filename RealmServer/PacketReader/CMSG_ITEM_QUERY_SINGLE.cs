using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     CMSG_ITEM_QUERY_SINGLE represents a packet sent by the client when it wants to retrieve item information.
    /// </summary>
    public sealed class CMSG_ITEM_QUERY_SINGLE : Common.Network.PacketReader
    {
        public ulong ItemId;
        public uint ItemIdo;

        public CMSG_ITEM_QUERY_SINGLE(byte[] data) : base(data)
        {
            ItemIdo = ReadUInt32();
            ItemId = ReadUInt64();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ITEM_QUERY_SINGLE] ItemIdo: {ItemIdo} ItemId: {ItemId}");
#endif
        }
    }
}