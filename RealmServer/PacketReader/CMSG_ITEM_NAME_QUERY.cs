using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_ITEM_NAME_QUERY : Common.Network.PacketReader
    {
        public uint ItemId;

        public CMSG_ITEM_NAME_QUERY(byte[] data) : base(data)
        {
            ItemId = ReadUInt32();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_ITEM_NAME_QUERY] ItemId: {ItemId}");
#endif
        }
    }
}