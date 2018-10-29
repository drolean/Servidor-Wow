using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUTOSTORE_LOOT_ITEM : Common.Network.PacketReader
    {
        public byte Slot;

        public CMSG_AUTOSTORE_LOOT_ITEM(byte[] data) : base(data)
        {
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUTOSTORE_LOOT_ITEM] Slot: {Slot}");
#endif
        }
    }
}
