using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUCTION_LIST_ITEMS : Common.Network.PacketReader
    {
        public ulong AuctioneerId;

        public uint InventoryType;
        public bool IsUsable;
        public uint ItemClass;
        public uint ItemSubClass;
        public byte LevelMax;

        public byte LevelMin;
        public int Quality;
        public string Query;

        public uint StartIndex;

        public CMSG_AUCTION_LIST_ITEMS(byte[] data) : base(data)
        {
            AuctioneerId = ReadUInt64();

            StartIndex = ReadUInt32();
            Query = ReadCString();

            LevelMin = ReadByte();
            LevelMax = ReadByte();

            InventoryType = ReadUInt32();
            ItemClass = ReadUInt32();
            ItemSubClass = ReadUInt32();
            Quality = ReadInt32();
            IsUsable = ReadBoolean();

#if DEBUG
            Log.Print(LogType.Debug,
                $"[CMSG_AUCTION_LIST_ITEMS] AuctioneerId: {AuctioneerId} StartIndex: {StartIndex} " +
                $"Query: {Query} LevelMin: {LevelMin} LevelMax: {LevelMax} InventoryType: {InventoryType} " +
                $"ItemClass: {ItemClass} ItemSubClass: {ItemSubClass} Quality: {Quality} IsUsable: {IsUsable}");
#endif
        }
    }
}
