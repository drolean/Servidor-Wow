using Common.Helpers;

namespace RealmServer.PacketReader
{
    /// <summary>
    ///     Auto-equips an item (can be triggered by double click etc)
    /// </summary>
    public sealed class CMSG_AUTOEQUIP_ITEM : Common.Network.PacketReader
    {
        public byte Bag;
        public byte Slot;

        public CMSG_AUTOEQUIP_ITEM(byte[] data) : base(data)
        {
            Bag = ReadByte();
            Slot = ReadByte();
#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUTOEQUIP_ITEM] Bag: {Bag} Slot: {Slot}");
#endif
        }
    }
}
