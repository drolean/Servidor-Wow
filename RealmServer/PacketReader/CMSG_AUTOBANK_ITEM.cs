using Common.Helpers;

namespace RealmServer.PacketReader
{
    public sealed class CMSG_AUTOBANK_ITEM : Common.Network.PacketReader
    {
        public byte BagSlot;
        public byte Slot;

        public CMSG_AUTOBANK_ITEM(byte[] data) : base(data)
        {
            BagSlot = ReadByte();
            Slot = ReadByte();

#if DEBUG
            Log.Print(LogType.Debug, $"[CMSG_AUTOBANK_ITEM] BagSlot: {BagSlot} Slot: {Slot}");
#endif
        }
    }
}