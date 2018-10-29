using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUTOSTORE_BANK_ITEM : Common.Network.PacketReader
    {
        public byte BagSlot;
        public byte Slot;

        public CMSG_AUTOSTORE_BANK_ITEM(byte[] data) : base(data)
        {
            BagSlot = ReadByte();
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUTOSTORE_BANK_ITEM] BagSlot: {BagSlot} Slot: {Slot}");
#endif
        }
    }
}
