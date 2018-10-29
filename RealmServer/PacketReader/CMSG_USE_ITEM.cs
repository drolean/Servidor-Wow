using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Use an Item
    /// </summary>
    public sealed class CMSG_USE_ITEM : Common.Network.PacketReader
    {
        public byte Bag;
        public byte Slot;
        public byte Tmp;

        public CMSG_USE_ITEM(byte[] data) : base(data)
        {
            Bag = ReadByte();
            Slot = ReadByte();
            Tmp = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_USE_ITEM] Bag: {Bag} Slot: {Slot} Tmp: {Tmp}");
#endif
        }
    }
}
