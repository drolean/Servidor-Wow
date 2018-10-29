using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUTOSTORE_BAG_ITEM : Common.Network.PacketReader
    {
        public byte Bag;
        public byte Dest;
        public byte Slot;

        public CMSG_AUTOSTORE_BAG_ITEM(byte[] data) : base(data)
        {
            Bag = ReadByte();
            Slot = ReadByte();
            Dest = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUTOSTORE_BAG_ITEM] Bag: {Bag} Slot: {Slot} Dest: {Dest}");
#endif
        }
    }
}
