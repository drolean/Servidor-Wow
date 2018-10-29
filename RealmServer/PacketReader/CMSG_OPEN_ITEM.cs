using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Open an Item
    /// </summary>
    public sealed class CMSG_OPEN_ITEM : Common.Network.PacketReader
    {
        public byte Bag;
        public byte Slot;

        public CMSG_OPEN_ITEM(byte[] data) : base(data)
        {
            Bag = ReadByte();
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_OPEN_ITEM] Bag: {Bag} Slot: {Slot}");
#endif
        }
    }
}
