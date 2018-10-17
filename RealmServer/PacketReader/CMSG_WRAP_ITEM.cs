using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_WRAP_ITEM : Common.Network.PacketReader
    {
        public byte GiftBag;
        public byte GiftSlot;
        public byte ItemBag;
        public byte ItemSlot;

        public CMSG_WRAP_ITEM(byte[] data) : base(data)
        {
            GiftBag = ReadByte();
            GiftSlot = ReadByte();
            ItemBag = ReadByte();
            ItemSlot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_WRAP_ITEM] GiftBag: {GiftBag} GiftSlot: {GiftSlot} ItemBag: {ItemBag} ItemSlot: {ItemSlot}");
#endif
        }
    }
}